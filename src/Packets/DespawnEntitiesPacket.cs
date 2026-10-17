using System.Collections.Generic;
using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets;

/// <summary>
///  Packet for despawn entities.
/// </summary>
public sealed class DespawnEntitiesPacket : IGamePacket {
    /// <summary>
    ///  The entity ids.
    /// </summary>
    public List<int> EntityIds { get; } = [];
}
