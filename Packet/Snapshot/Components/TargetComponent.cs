using System.Numerics;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Packet.Snapshot.Components;

public class TargetComponent : INetworkedComponent {
    public int? EntityId;
    public float Distance;
    public Vector3 Direction;
    public float HealthRatio;

    public bool HasTarget => EntityId.HasValue;

    public void Serialize(NetDataWriter writer) {
        writer.Put(HasTarget);
        if (HasTarget) {
            writer.Put(EntityId!.Value);
            writer.Put(Distance);
            writer.Put(Direction.X);
            writer.Put(Direction.Y);
            writer.Put(Direction.Z);
            writer.Put(HealthRatio);
        }
    }

    public void Deserialize(NetDataReader reader) {
        var hasTarget = reader.GetBool();
        if (!hasTarget) {
            EntityId = null;
            Distance = 0f;
            Direction = Vector3.Zero;
            HealthRatio = 0f;
            return;
        }

        EntityId = reader.GetInt();
        Distance = reader.GetFloat();
        Direction = new Vector3(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
        HealthRatio = reader.GetFloat();
    }

    public NetworkedComponentType Type => NetworkedComponentType.Target;
}