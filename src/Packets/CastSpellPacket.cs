using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for casting a spell.
/// </summary>
public sealed class CastSpellPacket : IGamePacket {
    /// <summary>
    ///  The spell index.
    /// </summary>
    public byte SpellIndex { get; set; }
}
