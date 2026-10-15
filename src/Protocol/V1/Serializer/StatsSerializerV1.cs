using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Packets;

namespace Yggdrasilnet.NetworkV4.Protocol.V1.Serializer;

/// <summary>
///  Serializer for a stats packet.
/// </summary>
public sealed class StatsSerializerV1 : IPacketSerializer<StatsPacket> {
    private const int PayloadBytes =
        sizeof(float) + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(float) + sizeof(float) + sizeof(float);
    
    /// <inheritdoc/>
    public void Serialize(StatsPacket packet, ProtocolHelper helper, NetDataWriter writer) {
        writer.Put(packet.ActualTps);
        writer.Put(packet.TargetTps);
        writer.Put(packet.EntityCount);
        writer.Put(packet.PlayerCount);
        writer.Put(packet.AvgTickMs);
        writer.Put(packet.MaxTickMs);
        writer.Put(packet.BudgetMs);
    }

    /// <inheritdoc/>
    public void Deserialize(StatsPacket packet, ProtocolHelper helper, NetDataReader reader) {
        if (reader.AvailableBytes < PayloadBytes) {
            throw new PacketFormatException("stats payload too short");
        }

        packet.ActualTps = reader.GetFloat();
        packet.TargetTps = reader.GetInt();
        packet.EntityCount = reader.GetInt();
        packet.PlayerCount = reader.GetInt();
        packet.AvgTickMs = reader.GetFloat();
        packet.MaxTickMs = reader.GetFloat();
        packet.BudgetMs = reader.GetFloat();

        if (!float.IsFinite(packet.ActualTps)
            || !float.IsFinite(packet.AvgTickMs)
            || !float.IsFinite(packet.MaxTickMs)
            || !float.IsFinite(packet.BudgetMs)) {
            throw new PacketFormatException("stats payload invalid float values");
        }

        if (packet.TargetTps < 0
            || packet.EntityCount < 0
            || packet.PlayerCount < 0
            || packet.AvgTickMs < 0f
            || packet.MaxTickMs < 0f
            || packet.BudgetMs < 0f) {
            throw new PacketFormatException("stats payload invalid numeric ranges");
        }
    }
}
