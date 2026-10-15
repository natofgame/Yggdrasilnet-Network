using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a despawn entities packet.
/// </summary>
public sealed class DespawnEntitiesSerializerV1 : IPacketSerializer<DespawnEntitiesPacket> {
    private const int HeaderBytes = sizeof(ushort);
    private const int EntityIdBytes = sizeof(int);
    private const int MaxEntityIds = 4096;
    
    /// <inheritdoc/>
    public void Serialize(DespawnEntitiesPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(checked((ushort)packet.EntityIds.Count));
        foreach (var entityId in packet.EntityIds) {
            writer.Put(entityId);
        }
    }

    /// <inheritdoc/>
    public void Deserialize(DespawnEntitiesPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("despawn payload too short");
        }

        packet.EntityIds.Clear();
        var count = reader.GetUShort();
        if (count > MaxEntityIds) {
            throw new PacketFormatException("despawn entity count too large");
        }

        if (reader.AvailableBytes < count * EntityIdBytes) {
            throw new PacketFormatException("despawn entity ids truncated");
        }

        for (var i = 0; i < count; i++) {
            packet.EntityIds.Add(reader.GetInt());
        }
    }
}
