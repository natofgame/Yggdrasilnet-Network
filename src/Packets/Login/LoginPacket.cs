using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets.Login;

public class LoginPacket : IGamePacket {
    public string Name { get; set; } = string.Empty;
}