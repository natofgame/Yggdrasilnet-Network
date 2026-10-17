using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets;

/// <summary>
///  Packet for casting a spell.
/// </summary>
public sealed class CastSpellPacket : IGamePacket {
    /// <summary>
    ///  The spell index.
    /// </summary>
    public byte SpellIndex { get; set; }
}
