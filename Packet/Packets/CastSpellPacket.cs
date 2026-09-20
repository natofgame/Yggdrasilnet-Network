using LiteNetLib.Utils;
using Yggdrasilnet.Protocol.Packet;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class CastSpellPacket : IPacket {
    public PacketType PacketType => PacketType.CastSpell;
    
    public byte SpellIndex { get; set; }
    
    public void Serialize(NetDataWriter writer) {
        writer.Put(SpellIndex);
    }
    public void Deserialize(NetDataReader reader) {
        SpellIndex = reader.GetByte();
    }
}