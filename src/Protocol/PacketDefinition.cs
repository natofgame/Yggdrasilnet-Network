using System;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Protocol;

/// <summary>
///  Definition of a packet.
/// </summary>
/// <param name="id"></param>
/// <param name="serializer"></param>
/// <param name="deserializer"></param>
public sealed class PacketDefinition(
    int id,
    Action<IGamePacket, ProtocolHelper, NetDataWriter> serializer,
    Action<IGamePacket, ProtocolHelper, NetDataReader> deserializer
) {
    /// <summary>
    ///  The id of the packet.
    /// </summary>
    public int Id => id;

    /// <summary>
    ///  Serializes the packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="helper"></param>
    /// <param name="writer"></param>
    public void Serialize(IGamePacket packet, ProtocolHelper helper, NetDataWriter writer) {
        serializer(packet, helper, writer);
    }

    /// <summary>
    ///  Deserializes the packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="helper"></param>
    /// <param name="reader"></param>
    public void Deserialize(IGamePacket packet, ProtocolHelper helper, NetDataReader reader) {
        deserializer(packet, helper, reader);
    }
}
