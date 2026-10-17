using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets.Protocol;

/// <summary>
///  Packet for a protocol accepted.
/// </summary>
public sealed class ProtocolAcceptedPacket : IGamePacket {
    /// <summary>
    ///  The protocol version.
    /// </summary>
    public int ProtocolVersion { get; set; }
}
