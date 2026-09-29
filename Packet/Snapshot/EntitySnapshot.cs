using System;
using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Snapshot;

public sealed class EntitySnapshot {
    public const int HeaderBytes = 23;
    public const int ComponentHeaderBytes = 3;
    public const int MaxComponentsPerEntity = 32;
    
    public int EntityId { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public List<INetworkedComponent> Components { get; set; } = new();
    public int EstimatedBytes { get; set; }

    public uint LastInputSequence { get; set; }
    
    public byte DefinitionId { get; set; }

    public void WriteTo(NetDataWriter writer) {
        writer.Put(EntityId);
        writer.Put(PositionX);
        writer.Put(PositionY);
        writer.Put(PositionZ);
        writer.Put(LastInputSequence);
        writer.Put(DefinitionId);

        writer.Put((ushort)Components.Count);
        foreach (var component in Components) {
            writer.Put((byte)component.Type);

            var lengthPos = writer.Length;
            writer.Put((ushort)0);
            var payloadStart = writer.Length;

            component.Serialize(writer);

            var payloadLength = writer.Length - payloadStart;
            if (payloadLength > ushort.MaxValue) {
                throw new InvalidOperationException($"Component {component.Type} too large ({payloadLength} bytes).");
            }

            FastBitConverter.GetBytes(writer.Data, lengthPos, (ushort)payloadLength);
        }
    }
    public static bool TryReadFrom(NetDataReader reader, NetworkedComponentRegistry registry,
        out EntitySnapshot snapshot) {
        snapshot = null!;

        if (reader.AvailableBytes < HeaderBytes) {
            return false;
        }

        var result = new EntitySnapshot {
            EntityId = reader.GetInt(),
            PositionX = reader.GetFloat(),
            PositionY = reader.GetFloat(),
            PositionZ = reader.GetFloat(),
            LastInputSequence = reader.GetUInt(),
            DefinitionId = reader.GetByte(),
        };

        int count = reader.GetUShort();
        if (count > MaxComponentsPerEntity) {
            return false;
        }

        for (var i = 0; i < count; i++) {
            if (reader.AvailableBytes < ComponentHeaderBytes) {
                return false;
            }

            var typeByte = reader.GetByte();
            int length = reader.GetUShort();
            if (length > reader.AvailableBytes) {
                return false;
            }

            var end = reader.Position + length;

            if (!Enum.IsDefined(typeof(NetworkedComponentType), typeByte)
                || !registry.TryCreate((NetworkedComponentType)typeByte, out var component)) {
                reader.SkipBytes(length);
                continue;
            }

            try {
                component.Deserialize(reader);
            } catch (Exception ex) when (ex is IndexOutOfRangeException or ArgumentException) {
                return false;
            }

            if (reader.Position != end) {
                return false;
            }

            result.Components.Add(component);
        }

        snapshot = result;
        return true;
    }
}
