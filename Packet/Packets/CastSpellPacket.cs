using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class CastSpellPacket : IPacket {
    private const int PayloadBytes = sizeof(byte);

    public PacketType PacketType => PacketType.CastSpell;
    
    public byte SpellIndex { get; set; }
    
    public void Serialize(NetDataWriter writer) {
        writer.Put(SpellIndex);
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < PayloadBytes) {
            throw new PacketFormatException("cast spell payload too short");
        }

        SpellIndex = reader.GetByte();
    }
}