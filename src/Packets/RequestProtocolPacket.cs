using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for requesting a protocol.
/// </summary>
public sealed class RequestProtocolPacket : IGamePacket {
    /// <summary>
    ///  The protocol version.
    /// </summary>
    public int ProtocolVersion { get; set; }
}
