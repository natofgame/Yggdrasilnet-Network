using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Snapshot.Components;

public sealed class ProjectileComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Projectile;

    public string SpellId { get; set; } = string.Empty;
    public int CasterEntityId { get; set; }
    public int TargetEntityId { get; set; }

    /// <inheritdoc/>
    public void Serialize(NetDataWriter writer) {
        writer.Put(SpellId);
        writer.Put(CasterEntityId);
        writer.Put(TargetEntityId);
    }
    
    /// <inheritdoc/>
    public void Deserialize(NetDataReader reader) {
        SpellId = reader.GetString();
        CasterEntityId = reader.GetInt();
        TargetEntityId = reader.GetInt();
    }
}
