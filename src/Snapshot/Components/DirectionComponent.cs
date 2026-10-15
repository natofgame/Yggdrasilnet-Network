using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Snapshot.Components;

public sealed class DirectionComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Direction;

    public float X { get; set; }
    public float Z { get; set; }

    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(X);
        writer.Put(Z);
    }
    
    /// <inheritdoc/>
    public void Deserialize(NetDataReader reader) {
        X = reader.GetFloat();
        Z = reader.GetFloat();
    }
}
