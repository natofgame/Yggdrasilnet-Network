using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a spellbook state packet.
/// </summary>
public sealed class SpellbookStateSerializerV1 : IPacketSerializer<SpellbookStatePacket> {
    // TODO: make this configurable
    private const int HeaderBytes = sizeof(int) + sizeof(ushort);
    private const int MinEntryBytes = sizeof(byte) + sizeof(ushort) + sizeof(float) + sizeof(int) + sizeof(int);
    private const int MaxEntries = 64;
    private const int MaxSpellIdLength = 64;
    
    /// <inheritdoc/>
    public void Serialize(SpellbookStatePacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.EntityId);
        writer.Put((ushort)packet.Spells.Count);
        foreach (var spell in packet.Spells) {
            writer.Put(spell.SlotIndex);
            writer.Put(spell.SpellId);
            writer.Put(spell.CooldownRemainingSeconds);
            writer.Put(spell.ChargesCurrent);
            writer.Put(spell.StackCount);
            writer.Put(spell.Version);
        }
    }
    
    /// <inheritdoc/>
    public void Deserialize(SpellbookStatePacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("spellbook state payload too short");
        }

        packet.EntityId = reader.GetInt();
        if (packet.EntityId < 0) {
            throw new PacketFormatException("spellbook state invalid entity id");
        }

        var count = reader.GetUShort();
        if (count > MaxEntries) {
            throw new PacketFormatException("spellbook state entry count too large");
        }

        packet.Spells.Clear();
        for (var i = 0; i < count; i++) {
            if (reader.AvailableBytes < MinEntryBytes) {
                throw new PacketFormatException("spellbook state truncated");
            }

            var slot = reader.GetByte();
            var spellId = reader.GetString();
            var cooldown = reader.GetFloat();
            var charges = reader.GetInt();
            var stacks = reader.GetInt();
            var version = reader.GetInt();

            if (string.IsNullOrWhiteSpace(spellId) || spellId.Length > MaxSpellIdLength) {
                throw new PacketFormatException("spellbook state invalid spell id");
            }

            if (!float.IsFinite(cooldown) || cooldown < 0f || charges < 0 || stacks < 0 || version < 0) {
                throw new PacketFormatException("spellbook state invalid numeric range");
            }

            packet.Spells.Add(new SpellSlotRuntimeNetState {
                SlotIndex = slot,
                SpellId = spellId,
                CooldownRemainingSeconds = cooldown,
                ChargesCurrent = charges,
                StackCount = stacks,
                Version = version
            });
        }
    }
}
