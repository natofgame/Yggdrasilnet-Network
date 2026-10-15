using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a command packet.
/// </summary>
public class CommandSerializerV1 : IPacketSerializer<CommandPacket> {
    /// <inheritdoc/>
    public void Serialize(CommandPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.EntityId);
        writer.Put(packet.Command);
    }
    
    /// <inheritdoc/>
    public void Deserialize(CommandPacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.EntityId = reader.GetInt();
        packet.Command = reader.GetString();
    }
}