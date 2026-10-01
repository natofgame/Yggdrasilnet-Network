using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class StatsPacket : IPacket {
    private const int PayloadBytes =
        sizeof(float) + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(float) + sizeof(float) + sizeof(float);

    public PacketType PacketType => PacketType.Stats;

    public float ActualTps { get; set; }
    public int TargetTps { get; set; }
    public int EntityCount { get; set; }
    public int PlayerCount { get; set; }
    public float AvgTickMs { get; set; }
    public float MaxTickMs { get; set; }
    public float BudgetMs { get; set; }

    public void Serialize(NetDataWriter writer) {
        writer.Put(ActualTps);
        writer.Put(TargetTps);
        writer.Put(EntityCount);
        writer.Put(PlayerCount);
        writer.Put(AvgTickMs);
        writer.Put(MaxTickMs);
        writer.Put(BudgetMs);
    }

    public void Deserialize(NetDataReader reader) {
        if (reader.AvailableBytes < PayloadBytes) {
            throw new PacketFormatException("stats payload too short");
        }

        ActualTps = reader.GetFloat();
        TargetTps = reader.GetInt();
        EntityCount = reader.GetInt();
        PlayerCount = reader.GetInt();
        AvgTickMs = reader.GetFloat();
        MaxTickMs = reader.GetFloat();
        BudgetMs = reader.GetFloat();

        if (float.IsNaN(ActualTps) || float.IsInfinity(ActualTps)
            || float.IsNaN(AvgTickMs) || float.IsInfinity(AvgTickMs)
            || float.IsNaN(MaxTickMs) || float.IsInfinity(MaxTickMs)
            || float.IsNaN(BudgetMs) || float.IsInfinity(BudgetMs)) {
            throw new PacketFormatException("stats payload invalid float values");
        }

        if (TargetTps < 0 || EntityCount < 0 || PlayerCount < 0 || AvgTickMs < 0f || MaxTickMs < 0f || BudgetMs < 0f) {
            throw new PacketFormatException("stats payload invalid numeric ranges");
        }
    }
}
