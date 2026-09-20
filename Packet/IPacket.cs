using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet;

public interface IPacket : INetSerializable {
    PacketType PacketType { get; }
}