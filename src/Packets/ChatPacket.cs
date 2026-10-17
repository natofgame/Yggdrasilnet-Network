using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets;

/// <summary>
///  Packet for chat.
/// </summary>
public class ChatPacket : IGamePacket {
    /// <summary>
    ///  The entity id.
    /// </summary>
    public int EntityId { get; set; }
    
    /// <summary>
    ///  The message.
    /// </summary>
    public string Message { get; set; } = "";
}