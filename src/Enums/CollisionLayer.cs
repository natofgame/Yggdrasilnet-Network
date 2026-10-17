using System;

namespace Yggdrasilnet.Network.Enums;

/// <summary>
///  Represents a collision layer.
/// </summary>
[Flags]
public enum CollisionLayer : uint {
    None = 0,
    World = 1 << 0,
    Player = 1 << 1,
    Monster = 1 << 2,
    Spell = 1 << 3,
    All = 0xFFFFFFFF
}
