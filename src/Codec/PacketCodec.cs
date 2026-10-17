using LiteNetLib.Utils;
using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Codec;

/// <summary>
///  Codec for packets.
/// </summary>
/// <param name="protocol"></param>
public sealed class PacketCodec(GameProtocol protocol) {
    public GameProtocol Protocol { get; private set; } = protocol;

    /// <summary>
    ///  Sets the protocol to use.
    /// </summary>
    /// <param name="protocolToUse"></param>
    public void SetProtocol(GameProtocol protocolToUse) {
        Protocol = protocolToUse;
    }

    /// <summary>
    ///  Tries to decode the packet.
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="packet"></param>
    /// <returns></returns>
    public bool TryDecode(byte[] payload, out IGamePacket? packet) {
        var reader = new NetDataReader(payload);
        return Protocol.TryDecode(reader, out packet);
    }

    /// <summary>
    ///  Tries to encode the packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="writer"></param>
    /// <returns></returns>
    public bool TryEncode(IGamePacket packet, NetDataWriter writer) {
        writer.Reset();
        return Protocol.TryEncode(packet, writer);
    }
}
