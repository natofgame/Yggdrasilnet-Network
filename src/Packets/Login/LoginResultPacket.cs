using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets.Login;

public class LoginResultPacket : IGamePacket {
    public bool Success { get; set; }

    public string PlayerId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;
}
