using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a cast spell packet.
/// </summary>
public sealed class CastSpellSerializerV1 : IPacketSerializer<CastSpellPacket> {
    /// <inheritdoc/>
    public void Serialize(CastSpellPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.SpellIndex);
    }
    
    /// <inheritdoc/>
    public void Deserialize(CastSpellPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < sizeof(byte)) {
            throw new PacketFormatException("cast spell payload too short");
        }

        packet.SpellIndex = reader.GetByte();
    }
}
