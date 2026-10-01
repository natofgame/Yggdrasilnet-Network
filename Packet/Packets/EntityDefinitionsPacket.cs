using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class EntityDefinitionsPacket : IPacket {
    private const int HeaderBytes = sizeof(ushort);
    private const int MinEntryBytes = sizeof(byte) + sizeof(ushort);
    private const int MaxDefinitions = 1024;
    private const int MaxDefinitionIdLength = 128;

    public PacketType PacketType => PacketType.EntityDefinitions;

    public List<EntityDefinitionEntry> Definitions { get; set; } = new();

    public void Serialize(NetDataWriter writer) {
        writer.Put((ushort)Definitions.Count);
        foreach (var entry in Definitions) {
            writer.Put(entry.Id);
            writer.Put(entry.DefinitionId);
        }
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("entity definitions payload too short");
        }

        var count = reader.GetUShort();
        if (count > MaxDefinitions) {
            throw new PacketFormatException("entity definitions count too large");
        }

        Definitions = new List<EntityDefinitionEntry>(count);
        for (var i = 0; i < count; i++) {
            if (reader.AvailableBytes < MinEntryBytes) {
                throw new PacketFormatException("entity definitions truncated");
            }

            var id = reader.GetByte();
            var definitionId = reader.GetString();
            if (string.IsNullOrWhiteSpace(definitionId) || definitionId.Length > MaxDefinitionIdLength) {
                throw new PacketFormatException("entity definition id invalid");
            }

            Definitions.Add(new EntityDefinitionEntry(id, definitionId));
        }
    }
}

public readonly record struct EntityDefinitionEntry(byte Id, string DefinitionId);
