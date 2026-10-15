using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for a player connexion.
/// </summary>
public sealed class PlayerConnexionPacket : IGamePacket {
    /// <summary>
    ///  The player id.
    /// </summary>
    public int PlayerId { get; set; }
    
    /// <summary>
    ///  Whether the player is the owner of the entity.
    /// </summary>
    public bool IsOwner { get; set; }
    
    /// <summary>
    ///  The entity id.
    /// </summary>
    public int EntityId { get; set; }
}
