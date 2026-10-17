using LiteNetLib.Utils;
using Yggdrasilnet.Network.Enums;

namespace Yggdrasilnet.Network.Snapshot.Components;

public sealed class CollisionComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Collision;

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public bool IsTrigger { get; set; }
    public CollisionLayer Layer { get; set; } = CollisionLayer.World;
    public CollisionLayer Mask { get; set; } = CollisionLayer.All;

    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(X);
        writer.Put(Y);
        writer.Put(Z);
        writer.Put(IsTrigger);
        writer.Put((uint)Layer);
        writer.Put((uint)Mask);
    }
    /// <inheritdoc/>
    public void Deserialize(NetDataReader reader) {
        X = reader.GetFloat();
        Y = reader.GetFloat();
        Z = reader.GetFloat();
        IsTrigger = reader.GetBool();
        Layer = (CollisionLayer)reader.GetUInt();
        Mask = (CollisionLayer)reader.GetUInt();
    }
}
