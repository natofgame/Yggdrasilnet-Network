using System;
using System.Collections.Generic;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Snapshot;

/// <summary>
///  Splits a snapshot into chunks.
/// </summary>
public static class SnapshotChunker {
    /// <summary>
    ///  Splits a snapshot into chunks.
    /// </summary>
    /// <param name="frameId"></param>
    /// <param name="keyframe"></param>
    /// <param name="entities"></param>
    /// <param name="chunkTargetBytes"></param>
    /// <param name="chunkHeaderBytes"></param>
    /// <param name="estimateEntityBytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        foreach (var entity in entities) {
            var entityBytes = estimateEntityBytes(entity);
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

            current.Entities.Add(entity);
            bytes += entityBytes;
        }

        if (current.Entities.Count > 0) {
            chunks.Add(current);
        }

        if (chunks.Count > SnapshotChunkPacket.MaxChunksPerFrame) {
            return new SnapshotChunkingResult([], droppedOversizedEntities, true);
        }

        for (var i = 0; i < chunks.Count; i++) {
            chunks[i].ChunkIndex = (ushort)i;
            chunks[i].ChunkCount = (ushort)chunks.Count;
        }

        return new SnapshotChunkingResult(chunks, droppedOversizedEntities, false);
    }
}

/// <summary>
///  The result of chunking a snapshot.
/// </summary>
public readonly struct SnapshotChunkingResult {
    public SnapshotChunkingResult(List<SnapshotChunkPacket> chunks, int droppedOversizedEntities, bool droppedFrame) {
        Chunks = chunks;
        DroppedOversizedEntities = droppedOversizedEntities;
        DroppedFrame = droppedFrame;
    }

    public List<SnapshotChunkPacket> Chunks { get; }
    public int DroppedOversizedEntities { get; }
    public bool DroppedFrame { get; }
}
