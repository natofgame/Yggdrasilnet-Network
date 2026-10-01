using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Packets;

public sealed class InputPacket : IPacket {
    public PacketType PacketType => PacketType.Input;
    
    public uint Sequence { get; set; }
    public float MoveX { get; set; }
    public float MoveZ { get; set; }
    public float DeltaTime { get; set; }
    
    public void Serialize(NetDataWriter writer) {
        writer.Put(Sequence);
        writer.Put(MoveX);
        writer.Put(MoveZ);
        writer.Put(DeltaTime);
    }

    public void Deserialize(NetDataReader reader) {
        const int payloadBytes = sizeof(uint) + sizeof(float) + sizeof(float) + sizeof(float);
        if (reader.AvailableBytes < payloadBytes) {
            throw new PacketFormatException("input payload too short");
        }

        Sequence = reader.GetUInt();
        MoveX = reader.GetFloat();
        MoveZ = reader.GetFloat();
        DeltaTime = reader.GetFloat();

        if (float.IsNaN(MoveX) || float.IsInfinity(MoveX)
            || float.IsNaN(MoveZ) || float.IsInfinity(MoveZ)
            || float.IsNaN(DeltaTime) || float.IsInfinity(DeltaTime)
            || DeltaTime < 0f) {
            throw new PacketFormatException("input payload invalid values");
        }
    }
}