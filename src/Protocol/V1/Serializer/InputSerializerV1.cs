using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for an input packet.
/// </summary>
public sealed class InputSerializerV1 : IPacketSerializer<InputPacket> {
    /// <inheritdoc/>
    public void Serialize(InputPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.MoveX);
        writer.Put(packet.MoveZ);
        writer.Put(packet.Sequence);
    }
    
    /// <inheritdoc/>
    public void Deserialize(InputPacket packet, ProtocolHelper helper, NetDataReader reader) {
        const int payloadBytes = sizeof(float) + sizeof(float) + sizeof(uint);
        if (reader.AvailableBytes < payloadBytes) {
            throw new PacketFormatException("input payload too short");
        }

        packet.MoveX = reader.GetFloat();
        packet.MoveZ = reader.GetFloat();
        packet.Sequence = reader.GetUInt();
        if (!float.IsFinite(packet.MoveX) || !float.IsFinite(packet.MoveZ)) {
            throw new PacketFormatException("input payload invalid values");
        }
    }
}
