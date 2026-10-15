using System.Collections.Generic;
using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for despawn entities.
/// </summary>
public sealed class DespawnEntitiesPacket : IGamePacket {
    /// <summary>
    ///  The entity ids.
    /// </summary>
    public List<int> EntityIds { get; } = [];
}
