using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets.Login;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a login result packet.
/// </summary>
public sealed class LoginResultSerializerV1 : IPacketSerializer<LoginResultPacket> {
    /// <inheritdoc/>
    public void Serialize(LoginResultPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.Success);
        writer.Put(packet.PlayerId);
        writer.Put(packet.Name);
        writer.Put(packet.Reason);
    }

    /// <inheritdoc/>
    public void Deserialize(LoginResultPacket packet, ProtocolHelper helper, NetDataReader reader) {
        packet.Success = reader.GetBool();
        packet.PlayerId = reader.GetString();
        packet.Name = reader.GetString();
        packet.Reason = reader.GetString();
    }
}
