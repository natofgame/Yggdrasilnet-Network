using System;
using System.Collections.Generic;
using Yggdrasilnet.Network.Packet.Packets;

namespace Yggdrasilnet.Network.Packet.Snapshot;

public static class SnapshotChunker {
    public static SnapshotChunkingResult Split(
        uint frameId,
        bool keyframe,
        IReadOnlyList<EntitySnapshot> entities,
        int chunkTargetBytes,
        int chunkHeaderBytes,
        Func<EntitySnapshot, int> estimateEntityBytes
    ) {
        if (estimateEntityBytes == null) {
            throw new ArgumentNullException(nameof(estimateEntityBytes));
        }

        if (chunkTargetBytes <= chunkHeaderBytes) {
            throw new ArgumentOutOfRangeException(nameof(chunkTargetBytes),
                "chunkTargetBytes must be greater than chunkHeaderBytes.");
        }

        var chunks = new List<SnapshotChunkPacket>();
        if (entities.Count == 0) {
            return new SnapshotChunkingResult(chunks, 0, false);
        }

        var maxEntityBudget = chunkTargetBytes - chunkHeaderBytes;
        var droppedOversizedEntities = 0;
        var current = new SnapshotChunkPacket { FrameId = frameId, IsKeyframe = keyframe };
        var bytes = chunkHeaderBytes;

        foreach (var e in entities) {
            var entityBytes = estimateEntityBytes(e);
            if (entityBytes > maxEntityBudget) {
                droppedOversizedEntities++;
                continue;
            }

            if (current.Entities.Count > 0
                && (bytes + entityBytes > chunkTargetBytes
                    || current.Entities.Count >= SnapshotChunkPacket.MaxEntitiesPerChunk)) {
                chunks.Add(current);
                current = new SnapshotChunkPacket { FrameId = frameId, IsKeyframe = keyframe };
                bytes = chunkHeaderBytes;
            }

            current.Entities.Add(e);
            bytes += entityBytes;
        }

        if (current.Entities.Count > 0) chunks.Add(current);

        if (chunks.Count > SnapshotChunkPacket.MaxChunksPerFrame) {
            return new SnapshotChunkingResult(new List<SnapshotChunkPacket>(), droppedOversizedEntities, true);
        }

        for (var i = 0; i < chunks.Count; i++) {
            chunks[i].ChunkIndex = (ushort)i;
            chunks[i].ChunkCount = (ushort)chunks.Count;
        }

        return new SnapshotChunkingResult(chunks, droppedOversizedEntities, false);
    }
}

public readonly record struct SnapshotChunkingResult(
    List<SnapshotChunkPacket> Chunks,
    int DroppedOversizedEntities,
    bool DroppedFrame
);