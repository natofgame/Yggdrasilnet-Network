using LiteNetLib.Utils;
using Yggdrasilnet.Network.Packets;

namespace Yggdrasilnet.Network.Protocol.V1.Serializer;

/// <summary>
///  Serializer for an entity definitions packet.
/// </summary>
public sealed class EntityDefinitionsSerializerV1 : IPacketSerializer<EntityDefinitionsPacket> {
    private const int HeaderBytes = sizeof(ushort);
    private const int MinEntryBytes = sizeof(byte) + sizeof(ushort);
    private const int MaxDefinitions = 1024;
    private const int MaxDefinitionIdLength = 128;
    
    /// <inheritdoc/>
    public void Serialize(EntityDefinitionsPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put((ushort)packet.Definitions.Count);
        foreach (var entry in packet.Definitions) {
            writer.Put(entry.Id);
            writer.Put(entry.DefinitionId);
        }
    }
 
    /// <inheritdoc/>
    public void Deserialize(EntityDefinitionsPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < HeaderBytes) {
            throw new PacketFormatException("entity definitions payload too short");
        }

        var count = reader.GetUShort();
        if (count > MaxDefinitions) {
            throw new PacketFormatException("entity definitions count too large");
        }

        packet.Definitions.Clear();
        for (var i = 0; i < count; i++) {
            if (reader.AvailableBytes < MinEntryBytes) {
                throw new PacketFormatException("entity definitions truncated");
            }

            var id = reader.GetByte();
            var definitionId = reader.GetString();
            if (string.IsNullOrWhiteSpace(definitionId) || definitionId.Length > MaxDefinitionIdLength) {
                throw new PacketFormatException("entity definition id invalid");
            }

            packet.Definitions.Add(new EntityDefinitionEntry(id, definitionId));
        }
    }
}
