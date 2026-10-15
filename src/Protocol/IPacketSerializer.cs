using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Protocol;

/// <summary>
///  Serializer for a packet.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPacketSerializer<in T> where T : IGamePacket {
    /// <summary>
    ///  Serializes the packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="helper"></param>
    /// <param name="writer"></param>
    void Serialize(T packet, ProtocolHelper helper, NetDataWriter writer);
    
    /// <summary>
    ///  Deserializes the packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="helper"></param>
    /// <param name="reader"></param>
    void Deserialize(T packet, ProtocolHelper helper, NetDataReader reader);
}
