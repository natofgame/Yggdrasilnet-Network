using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class DespawnEntitiesPacket : IPacket {
    private const int HeaderBytes = sizeof(ushort);
    private const int EntityIdBytes = sizeof(int);
    private const int MaxEntityIds = 4096;

    public PacketType PacketType => PacketType.DespawnEntities;

    public List<int> EntityIds { get; set; } = new();

    public void Serialize(NetDataWriter writer) {
        writer.Put(checked((ushort)EntityIds.Count));
        foreach (var entityId in EntityIds) {
            writer.Put(entityId);
        }
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("despawn payload too short");
        }

        EntityIds.Clear();
        var count = reader.GetUShort();
        if (count > MaxEntityIds) {
            throw new PacketFormatException("despawn entity count too large");
        }

        if (reader.AvailableBytes < count * EntityIdBytes) {
            throw new PacketFormatException("despawn entity ids truncated");
        }

        for (var i = 0; i < count; i++) {
            EntityIds.Add(reader.GetInt());
        }
    }
}
