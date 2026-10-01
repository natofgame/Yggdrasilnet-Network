using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class SpawnEntitiesPacket : IPacket {
    private const int MinPayloadBytes = sizeof(ushort) + sizeof(int);
    private const int MaxDefinitionIdLength = 128;
    private const int MaxSpawnCount = 10000;

    public PacketType PacketType => PacketType.SpawnEntities;

    public string DefinitionId { get; set; } = "crowd";
    public int Count { get; set; }

    public void Serialize(NetDataWriter writer) {
        writer.Put(DefinitionId);
        writer.Put(Count);
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < MinPayloadBytes) {
            throw new PacketFormatException("spawn entities payload too short");
        }

        DefinitionId = reader.GetString();
        Count = reader.GetInt();

        if (string.IsNullOrWhiteSpace(DefinitionId) || DefinitionId.Length > MaxDefinitionIdLength) {
            throw new PacketFormatException("spawn entities definition id invalid");
        }

        if (Count < 0 || Count > MaxSpawnCount) {
            throw new PacketFormatException("spawn entities count invalid");
        }
    }
}
