using System;
using System.Collections.Generic;
using Yggdrasilnet.Network.Packet.Packets;

namespace Yggdrasilnet.Network.Packet.Snapshot;

public static class SnapshotChunker {
    public static List<SnapshotChunkPacket> Split(uint frameId, bool keyframe,
        IReadOnlyList<EntitySnapshot> entities, int maxPayloadBytes) {
        var chunks = new List<SnapshotChunkPacket>();
        var current = new SnapshotChunkPacket { FrameId = frameId, IsKeyframe = keyframe };
        var bytes = 0;

        foreach (var e in entities) {
            if (current.Entities.Count > 0
                && (bytes + e.EstimatedBytes > maxPayloadBytes
                    || current.Entities.Count >= SnapshotChunkPacket.MaxEntitiesPerChunk)) {
                chunks.Add(current);
                current = new SnapshotChunkPacket { FrameId = frameId, IsKeyframe = keyframe };
                bytes = 0;
            }
            current.Entities.Add(e);
            bytes += e.EstimatedBytes;
        }
        if (current.Entities.Count > 0) chunks.Add(current);

        for (var i = 0; i < chunks.Count; i++) {
            chunks[i].ChunkIndex = (ushort)i;
            chunks[i].ChunkCount = (ushort)chunks.Count;
        }
        
        if (chunks.Count > SnapshotChunkPacket.MaxChunksPerFrame) {
            throw new InvalidOperationException(
                $"Snapshot frame {frameId} needs {chunks.Count} chunks (max {SnapshotChunkPacket.MaxChunksPerFrame}).");
        }
        return chunks;
    }
}