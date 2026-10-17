using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a command suggestion request packet.
/// </summary>
public sealed class CommandSuggestionRequestSerializerV1 : IPacketSerializer<CommandSuggestionRequestPacket> {
    private const int MaxTextLength = 512;

    /// <inheritdoc/>
    public void Serialize(CommandSuggestionRequestPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.RequestId);
        writer.Put(packet.Text, MaxTextLength);
    }

    /// <inheritdoc/>
    public void Deserialize(CommandSuggestionRequestPacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.RequestId = reader.GetInt();
        packet.Text = reader.GetString(MaxTextLength);
    }
}
