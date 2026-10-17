using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Snapshot.Components;

public sealed class HealthComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Health;

    public float Current { get; set; }
    public float Max { get; set; }

    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(Current);
        writer.Put(Max);
    }

    /// <inheritdoc/>
    public void Deserialize(NetDataReader reader) {
        Current = reader.GetFloat();
        Max = reader.GetFloat();
    }
}
