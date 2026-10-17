using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets.Login;

public class LoginPacket : IGamePacket {
    public string Name { get; set; } = string.Empty;
}