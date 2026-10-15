using LiteNetLib.Utils;
using Yggdrasilnet.NetworkV4.Enums;

namespace Yggdrasilnet.NetworkV4.Snapshot.Components;

public sealed class ActionComponent : INetworkedComponent {
    /// <inheritdoc/>
    public NetworkedComponentType Type => NetworkedComponentType.Action;

    public byte ActionType { get; set; }
    public SpellPhase Phase { get; set; } = SpellPhase.None;
    public float PhaseProgress01 { get; set; }
    public string SpellId { get; set; } = string.Empty;
    public int TargetEntityId { get; set; } = -1;
    public SpellType SpellType { get; set; } = SpellType.Projectile;
    public bool HasDashEffect { get; set; }

    public void Serialize(NetDataWriter writer) {
        writer.Put(ActionType);
        writer.Put((byte)Phase);
        writer.Put(PhaseProgress01);
        writer.Put(SpellId);
        writer.Put(TargetEntityId);
        writer.Put((byte)SpellType);
        writer.Put(HasDashEffect);
    }

    public void Deserialize(NetDataReader reader) {
        ActionType = reader.GetByte();
        Phase = (SpellPhase)reader.GetByte();
        PhaseProgress01 = reader.GetFloat();
        SpellId = reader.GetString();
        TargetEntityId = reader.GetInt();
        SpellType = (SpellType)reader.GetByte();
        HasDashEffect = reader.GetBool();
    }
}
