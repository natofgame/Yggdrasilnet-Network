using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for input.
/// </summary>
public sealed class InputPacket : IGamePacket {
    /// <summary>
    ///  Move X
    /// </summary>
    public float MoveX { get; set; }
    
    /// <summary>
    ///  Move Z
    /// </summary>
    public float MoveZ { get; set; }
    
    /// <summary>
    ///  The input sequence.
    /// </summary>
    public uint Sequence { get; set; }
}
