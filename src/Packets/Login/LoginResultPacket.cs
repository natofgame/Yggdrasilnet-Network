using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets.Login;

public class LoginResultPacket : IGamePacket {
    public bool Success { get; set; }

    public string PlayerId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;
}
