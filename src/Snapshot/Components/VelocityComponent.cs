using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Snapshot.Components;

public sealed class VelocityComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Velocity;

    public float X { get; set;}
    public float Y { get; set; }
    public float Z { get; set; }

    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(X);
        writer.Put(Y);
        writer.Put(Z);
    }
    
    /// <inheritdoc/>
    public void Deserialize(NetDataReader reader) {
        X = reader.GetFloat();
        Y = reader.GetFloat();
        Z = reader.GetFloat();
    }
}
