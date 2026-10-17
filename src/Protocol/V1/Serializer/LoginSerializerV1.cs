using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets.Login;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a login packet.
/// </summary>
public sealed class LoginSerializerV1 : IPacketSerializer<LoginPacket> {
    /// <inheritdoc/>
    public void Serialize(LoginPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.Name);
    }

    /// <inheritdoc/>
    public void Deserialize(LoginPacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.Name = reader.GetString();
    }
}
