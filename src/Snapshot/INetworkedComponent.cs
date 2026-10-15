using LiteNetLib.Utils;

namespace Yggdrasilnet.NetworkV4.Snapshot;

/// <summary>
///  Represents a networked component.
/// </summary>
public interface INetworkedComponent : INetSerializable {
    /// <summary>
    ///  The type of the component.
    /// </summary>
    NetworkedComponentType Type { get; }
}
