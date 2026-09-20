using LiteNetLib.Utils;
using Yggdrasilnet.Protocol.Packet;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class SpawnEntitiesPacket : IPacket {
    public PacketType PacketType => PacketType.SpawnEntities;

    public string DefinitionId { get; set; } = "crowd";
    public int Count { get; set; }

    public void Serialize(NetDataWriter writer) {
        writer.Put(DefinitionId);
        writer.Put(Count);
    }

    public void Deserialize(NetDataReader reader) {
        DefinitionId = reader.GetString();
        Count = reader.GetInt();
    }
}
