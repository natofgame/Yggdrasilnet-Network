using LiteNetLib;

namespace Yggdrasilnet.Network.Packet;

public interface IPacketHandler<TPacket, in TContext> where TPacket : IPacket {
    public void Handle(NetPeer peer, TPacket packet, TContext context);
}