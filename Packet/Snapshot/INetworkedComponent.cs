using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Snapshot;

public interface INetworkedComponent : INetSerializable {
    NetworkedComponentType Type { get; }
}