using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

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