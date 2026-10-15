using System.Numerics;
using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Snapshot.Components;

public sealed class TargetComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Target;

    public int? EntityId;
    public float Distance;
    public Vector3 Direction;
    public float HealthRatio;

    /// <summary>
    ///  Whether the entity has a target.
    /// </summary>
    public bool HasTarget => EntityId.HasValue;
    
    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(HasTarget);
        if (!HasTarget) {
            return;
        }

        writer.Put(EntityId!.Value);
        writer.Put(Distance);
        writer.Put(Direction.X);
        writer.Put(Direction.Y);
        writer.Put(Direction.Z);
        writer.Put(HealthRatio);
    }
    
    /// <inheritdoc/>
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
}
