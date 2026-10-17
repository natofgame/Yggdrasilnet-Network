using System.Collections.Generic;
using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets;

/// <summary>
///  Packet for spellbook state.
/// </summary>
public sealed class SpellbookStatePacket : IGamePacket {
    /// <summary>
    ///  The entity id.
    /// </summary>
    public int EntityId { get; set; }
    
    /// <summary>
    ///  The spells.
    /// </summary>
    public List<SpellSlotRuntimeNetState> Spells { get; } = [];
}

// TODO: Use a proper serializer
public sealed class SpellSlotRuntimeNetState {
    public byte SlotIndex { get; set; }
    public string SpellId { get; set; } = string.Empty;
    public float CooldownRemainingSeconds { get; set; }
    public int ChargesCurrent { get; set; }
    public int StackCount { get; set; }
    public int Version { get; set; }
}
