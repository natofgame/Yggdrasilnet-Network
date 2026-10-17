using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets.Command;

/// <summary>
///  Packet for a command.
/// </summary>
public class CommandPacket : IGamePacket {
    /// <summary>
    ///  The entity id.
    /// </summary>
    public int EntityId { get; set; } = -1;
    
    /// <summary>
    ///  The command.
    /// </summary>
    public string Command { get; set; } = string.Empty;
}