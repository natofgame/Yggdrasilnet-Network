using System;
using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Snapshot;

public sealed class EntitySnapshot {
    // TODO: Moove to config
    public const int HeaderBytes = 23;
    public const int ComponentHeaderBytes = 3;
    private const int MaxComponentsPerEntity = 32;

    /// <summary>
    ///  The entity ID.
    /// </summary>
    public int EntityId { get; init; }
    
    /// <summary>
    ///  The entity position.
    /// </summary>
    public float PositionX { get; init; }
    
    /// <summary>
    ///  The entity position.
    /// </summary>
    public float PositionY { get; init; }
    
    /// <summary>
    ///  The entity position.
    /// </summary>
    public float PositionZ { get; init; }
    
    /// <summary>
    ///  The last input sequence.
    /// </summary>
    public uint LastInputSequence { get; init; }
    
    /// <summary>
    ///  The entity definition ID.
    /// </summary>
    public byte DefinitionId { get; init; }
    
    /// <summary>
    ///  The entity components.
    /// </summary>
    public List<INetworkedComponent> Components { get; } = [];
    
    public int EstimatedBytes { get; set; }

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

    /// <summary>
    ///  Tries to read an entity snapshot from the given reader.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="registry"></param>
    /// <param name="snapshot"></param>
    /// <returns></returns>
    public static bool TryReadFrom(NetDataReader reader, NetworkedComponentRegistry registry, out EntitySnapshot snapshot) {
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
            DefinitionId = reader.GetByte()
        };

        var count = reader.GetUShort();
        if (count > MaxComponentsPerEntity) {
            return false;
        }

        for (var i = 0; i < count; i++) {
            if (reader.AvailableBytes < ComponentHeaderBytes) {
                return false;
            }

            var typeByte = reader.GetByte();
            var length = reader.GetUShort();
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
            } catch (Exception exception) when (exception is IndexOutOfRangeException or ArgumentException) {
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
