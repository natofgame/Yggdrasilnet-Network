using System.Collections.Generic;
using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packet.Snapshot;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class SnapshotChunkPacket : IPacket {
    public const int MaxChunksPerFrame = 64;
    public const int MaxEntitiesPerChunk = 256;

    public PacketType PacketType => PacketType.SnapshotChunk;

    public uint FrameId { get; set; }
    public bool IsKeyframe { get; set; }
    public ushort ChunkIndex { get; set; }
    public ushort ChunkCount { get; set; }
    public List<EntitySnapshot> Entities { get; } = new();

    public void Serialize(NetDataWriter writer) {
        writer.Put(FrameId);
        writer.Put(IsKeyframe);
        writer.Put(ChunkIndex);
        writer.Put(ChunkCount);
        writer.Put((ushort)Entities.Count);
        foreach (var e in Entities) e.WriteTo(writer);
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < 4 + 1 + 2 + 2 + 2) {
            throw new PacketFormatException("chunk header");
        }

        FrameId = reader.GetUInt();
        IsKeyframe = reader.GetBool();
        ChunkIndex = reader.GetUShort();
        ChunkCount = reader.GetUShort();
        if (ChunkCount == 0 || ChunkCount > MaxChunksPerFrame || ChunkIndex >= ChunkCount) {
            throw new PacketFormatException("chunk index/count");
        }

        int count = reader.GetUShort();
        if (count > MaxEntitiesPerChunk) throw new PacketFormatException("entity count");

        Entities.Clear();
        for (var i = 0; i < count; i++) {
            if (!EntitySnapshot.TryReadFrom(reader, NetworkedComponentRegistry.Default, out var e)) {
                throw new PacketFormatException("entity");
            }
            Entities.Add(e);
        }
    }
}