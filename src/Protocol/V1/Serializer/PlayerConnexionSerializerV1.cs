using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a player connexion packet.
/// </summary>
public sealed class PlayerConnexionSerializerV1 : IPacketSerializer<PlayerConnexionPacket> {
    /// <inheritdoc/>
    public void Serialize(PlayerConnexionPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.PlayerId);
        writer.Put(packet.IsOwner);
        writer.Put(packet.EntityId);
    }
    
    /// <inheritdoc/>
    public void Deserialize(PlayerConnexionPacket packet, ProtocolHelper helper, NetDataReader reader) {
        const int payloadBytes = sizeof(int) + sizeof(byte) + sizeof(int);
        if (reader.AvailableBytes < payloadBytes) {
            throw new PacketFormatException("player connexion payload too short");
        }

        packet.PlayerId = reader.GetInt();
        packet.IsOwner = reader.GetBool();
        packet.EntityId = reader.GetInt();
        if (packet.PlayerId < 0 || packet.EntityId < -1) {
            throw new PacketFormatException("player connexion invalid ids");
        }
    }
}
