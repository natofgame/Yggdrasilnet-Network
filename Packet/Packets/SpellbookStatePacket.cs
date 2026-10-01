using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class SpellbookStatePacket : IPacket {
    private const int HeaderBytes = sizeof(int) + sizeof(ushort);
    private const int MinEntryBytes = sizeof(byte) + sizeof(ushort) + sizeof(float) + sizeof(int) + sizeof(int);
    private const int MaxEntries = 64;
    private const int MaxSpellIdLength = 64;

    public PacketType PacketType => PacketType.SpellbookState;

    public int EntityId { get; set; }
    public List<SpellSlotRuntimeNetState> Spells { get; } = new();

    public void Serialize(NetDataWriter writer) {
        writer.Put(EntityId);
        writer.Put((ushort)Spells.Count);
        foreach (var spell in Spells) {
            writer.Put(spell.SlotIndex);
            writer.Put(spell.SpellId);
            writer.Put(spell.CooldownRemainingSeconds);
            writer.Put(spell.ChargesCurrent);
            writer.Put(spell.StackCount);
            writer.Put(spell.Version);
        }
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("spellbook state payload too short");
        }

        EntityId = reader.GetInt();
        if (EntityId < 0) {
            throw new PacketFormatException("spellbook state invalid entity id");
        }

        var count = reader.GetUShort();
        if (count > MaxEntries) {
            throw new PacketFormatException("spellbook state entry count too large");
        }

        Spells.Clear();
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

            if (float.IsNaN(cooldown) || float.IsInfinity(cooldown) || cooldown < 0f || charges < 0 || stacks < 0 || version < 0) {
                throw new PacketFormatException("spellbook state invalid numeric range");
            }

            Spells.Add(new SpellSlotRuntimeNetState {
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

public sealed class SpellSlotRuntimeNetState {
    public byte SlotIndex { get; set; }
    public string SpellId { get; set; } = string.Empty;
    public float CooldownRemainingSeconds { get; set; }
    public int ChargesCurrent { get; set; }
    public int StackCount { get; set; }
    public int Version { get; set; }
}
