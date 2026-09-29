using System;
using System.Collections.Generic;
using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packet.Packets;

namespace Yggdrasilnet.Network.Packet;

public sealed class PacketRegistry {
    private readonly Dictionary<PacketType, Func<IPacket>> _factories = new();

    public PacketRegistry() {
        Register(PacketType.PlayerConnexion, () => new PlayerConnexionPacket());
        Register(PacketType.SnapshotChunk, () => new SnapshotChunkPacket());
        Register(PacketType.Input, () => new InputPacket());
        Register(PacketType.SpawnEntities, () => new SpawnEntitiesPacket());
        Register(PacketType.Stats, () => new StatsPacket());
        Register(PacketType.EntityDefinitions, () => new EntityDefinitionsPacket());
        Register(PacketType.CastSpell, () => new CastSpellPacket());
        Register(PacketType.DespawnEntities, () => new DespawnEntitiesPacket());
    }

    public void Register(PacketType type, Func<IPacket> factory) {
        _factories[type] = factory;
    }

    public bool TryCreate(PacketType type, out IPacket packet) {
        if (_factories.TryGetValue(type, out var factory)) {
            packet = factory();
            return true;
        }

        packet = null!;
        return false;
    }

    public bool TryRead(NetDataReader reader, out IPacket packet) {
        packet = null!;

        if (reader.AvailableBytes < 1) {
            return false;
        }

        var typeByte = reader.GetByte();
        if (!Enum.IsDefined(typeof(PacketType), typeByte)) {
            return false;
        }

        if (!TryCreate((PacketType)typeByte, out var created)) {
            return false;
        }

        try {
            created.Deserialize(reader);
        } catch (Exception ex) when (ex is PacketFormatException
                                         or IndexOutOfRangeException
                                         or ArgumentException
                                         or InvalidOperationException) {
            return false;
        }

        packet = created;
        return true;
    }

    public void Write(NetDataWriter writer, IPacket packet) {
        writer.Put((byte)packet.PacketType);
        packet.Serialize(writer);
    }
}