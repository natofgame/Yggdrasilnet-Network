using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;
using Yggdrasilnet.Network.Packets.Protocol;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a protocol accepted packet.
/// </summary>
public sealed class ProtocolAcceptedSerializerV1 : IPacketSerializer<ProtocolAcceptedPacket> {
    /// <inheritdoc/>
    public void Serialize(ProtocolAcceptedPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.ProtocolVersion);
    }
    
    /// <inheritdoc/>
    public void Deserialize(ProtocolAcceptedPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < sizeof(int)) {
            throw new PacketFormatException("protocol accepted payload too short");
        }

        packet.ProtocolVersion = reader.GetInt();
    }
}
