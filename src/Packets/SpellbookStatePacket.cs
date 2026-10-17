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

/// <summary>
///  Runtime state of one spell slot. Serialized by <c>SpellbookStateSerializerV1</c>.
/// </summary>
public sealed class SpellSlotRuntimeNetState {
    /// <summary>
    ///  The slot index in the spellbook.
    /// </summary>
    public byte SlotIndex { get; set; }

    /// <summary>
    ///  The spell id.
    /// </summary>
    public string SpellId { get; set; } = string.Empty;

    /// <summary>
    ///  The remaining cooldown, in seconds.
    /// </summary>
    public float CooldownRemainingSeconds { get; set; }

    /// <summary>
    ///  The number of charges currently available.
    /// </summary>
    public int ChargesCurrent { get; set; }

    /// <summary>
    ///  The current stack count.
    /// </summary>
    public int StackCount { get; set; }

    /// <summary>
    ///  Incremented each time the slot changes, so the client can drop stale updates.
    /// </summary>
    public int Version { get; set; }
}
