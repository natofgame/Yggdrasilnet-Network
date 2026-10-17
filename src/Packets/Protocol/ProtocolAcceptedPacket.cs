using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets.Protocol;

/// <summary>
///  Packet for a protocol accepted.
/// </summary>
public sealed class ProtocolAcceptedPacket : IGamePacket {
    /// <summary>
    ///  The protocol version.
    /// </summary>
    public int ProtocolVersion { get; set; }
}
