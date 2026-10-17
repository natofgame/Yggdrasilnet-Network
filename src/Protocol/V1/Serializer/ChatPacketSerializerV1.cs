using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a chat packet.
/// </summary>
public class ChatPacketSerializerV1 : IPacketSerializer<ChatPacket> {
    /// <inheritdoc/>
    public void Serialize(ChatPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.EntityId);
        writer.Put(packet.Message);
    }
    
    /// <inheritdoc/>
    public void Deserialize(ChatPacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.EntityId = reader.GetInt();
        packet.Message = reader.GetString();
    }
}