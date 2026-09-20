using LiteNetLib;
using Yggdrasilnet.Network.Packet;

namespace Yggdrasilnet.Network;

public interface IPacketHandler<TPacket, in TContext> where TPacket : IPacket {
    public void Handle(NetPeer peer, TPacket packet, TContext context);
}