using System.Collections.Generic;
using Yggdrasilnet.NetworkV4.Protocol;
using Yggdrasilnet.NetworkV4.Snapshot;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for a snapshot chunk.
/// </summary>
public sealed class SnapshotChunkPacket : IGamePacket {
    public const int HeaderBytes = 12;
    public const int MaxChunksPerFrame = 64;
    public const int MaxEntitiesPerChunk = 256;

    /// <summary>
    ///  The frame ID.
    /// </summary>
    public uint FrameId { get; set; }
    
    /// <summary>
    ///  Whether this chunk is a keyframe.
    /// </summary>
    public bool IsKeyframe { get; set; }
    
    /// <summary>
    ///  The chunk index.
    /// </summary>
    public ushort ChunkIndex { get; set; }
    
    /// <summary>
    ///  The chunk count.
    /// </summary>
    public ushort ChunkCount { get; set; }
    
    /// <summary>
    /// The entities.
    /// </summary>
    public List<EntitySnapshot> Entities { get; } = [];
}
