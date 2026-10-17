using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;
using Yggdrasilnet.Network.Packets.Command;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a command suggestion response packet.
/// </summary>
public sealed class CommandSuggestionResponseSerializerV1 : IPacketSerializer<CommandSuggestionResponsePacket> {
    private const int MaxSuggestions = 256;

    /// <inheritdoc/>
    public void Serialize(CommandSuggestionResponsePacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.RequestId);
        writer.Put(packet.Start);
        writer.Put(checked((ushort)packet.Suggestions.Count));
        foreach (var suggestion in packet.Suggestions) {
            writer.Put(suggestion);
        }

        writer.Put(packet.Usage);
    }

    /// <inheritdoc/>
    public void Deserialize(CommandSuggestionResponsePacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.RequestId = reader.GetInt();
        packet.Start = reader.GetInt();

        var count = reader.GetUShort();
        if (count > MaxSuggestions) {
            throw new PacketFormatException("suggestion count too large");
        }

        packet.Suggestions.Clear();
        for (var i = 0; i < count; i++) {
            packet.Suggestions.Add(reader.GetString());
        }

        packet.Usage = reader.GetString();
    }
}
