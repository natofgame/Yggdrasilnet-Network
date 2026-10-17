using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets.Protocol;

/// <summary>
///  Packet for requesting a protocol.
/// </summary>
public sealed class RequestProtocolPacket : IGamePacket {
    /// <summary>
    ///  The protocol version.
    /// </summary>
    public int ProtocolVersion { get; set; }
}
