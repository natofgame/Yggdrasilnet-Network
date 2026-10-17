using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;
using Yggdrasilnet.Network.Snapshot;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a snapshot chunk packet.
/// </summary>
public sealed class SnapshotChunkSerializerV1 : IPacketSerializer<SnapshotChunkPacket> {
    /// <inheritdoc/>
    public void Serialize(SnapshotChunkPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.FrameId);
        writer.Put(packet.IsKeyframe);
        writer.Put(packet.ChunkIndex);
        writer.Put(packet.ChunkCount);
        writer.Put((ushort)packet.Entities.Count);
        foreach (var entity in packet.Entities) {
            entity.WriteTo(writer);
        }
    }
    
    /// <inheritdoc/>
    public void Deserialize(SnapshotChunkPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < 4 + 1 + 2 + 2 + 2) {
            throw new PacketFormatException("chunk header");
        }

        packet.FrameId = reader.GetUInt();
        packet.IsKeyframe = reader.GetBool();
        packet.ChunkIndex = reader.GetUShort();
        packet.ChunkCount = reader.GetUShort();
        if (packet.ChunkCount == 0
            || packet.ChunkCount > SnapshotChunkPacket.MaxChunksPerFrame
            || packet.ChunkIndex >= packet.ChunkCount) {
            throw new PacketFormatException("chunk index/count");
        }

        var count = reader.GetUShort();
        if (count > SnapshotChunkPacket.MaxEntitiesPerChunk) {
            throw new PacketFormatException("entity count");
        }

        packet.Entities.Clear();
        for (var i = 0; i < count; i++) {
            if (!EntitySnapshot.TryReadFrom(reader, NetworkedComponentRegistry.Default, out var entity)) {
                throw new PacketFormatException("entity");
            }

            packet.Entities.Add(entity);
        }
    }
}
