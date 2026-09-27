namespace Yggdrasilnet.Network.Packet;

public interface IClientPacketHandler<in TPacket> where TPacket : IPacket {
    void Handle(TPacket packet);
}