using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Snapshot;

/// <summary>
///  Represents a networked component.
/// </summary>
public interface INetworkedComponent : INetSerializable {
    /// <summary>
    ///  The type of the component.
    /// </summary>
    NetworkedComponentType Type { get; }
}
