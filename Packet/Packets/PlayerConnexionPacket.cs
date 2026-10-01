using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class PlayerConnexionPacket : IPacket {
    private const int PayloadBytes = sizeof(int) + sizeof(byte) + sizeof(int);

    public PacketType PacketType => PacketType.PlayerConnexion;

    public int PlayerId { get; set; }
    public bool IsOwner { get; set; }
    public int EntityId { get; set; }

    public void Serialize(NetDataWriter writer) {
        writer.Put(PlayerId);
        writer.Put(IsOwner);
        writer.Put(EntityId);
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < PayloadBytes) {
            throw new PacketFormatException("player connexion payload too short");
        }

        PlayerId = reader.GetInt();
        IsOwner = reader.GetBool();
        EntityId = reader.GetInt();

        if (PlayerId < 0 || EntityId < -1) {
            throw new PacketFormatException("player connexion invalid ids");
        }
    }
}
