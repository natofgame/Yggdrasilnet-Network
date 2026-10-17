using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;
using Yggdrasilnet.NetworkV4.Packets.Protocol;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a request protocol packet.
/// </summary>
public sealed class RequestProtocolSerializerV1 : IPacketSerializer<RequestProtocolPacket> {
    /// <inheritdoc/>
    public void Serialize(RequestProtocolPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.ProtocolVersion);
    }
    
    /// <inheritdoc/>
    public void Deserialize(RequestProtocolPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < sizeof(int)) {
            throw new PacketFormatException("request protocol payload too short");
        }

        packet.ProtocolVersion = reader.GetInt();
    }
}
