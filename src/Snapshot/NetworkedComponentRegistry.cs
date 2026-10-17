using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Yggdrasilnet.Network.Snapshot.Components;

namespace Yggdrasilnet.Network.Snapshot;

// TODO: Refacto

/// <summary>
///  Registry for networked components.
/// </summary>
public sealed class NetworkedComponentRegistry {
    private readonly Dictionary<NetworkedComponentType, Func<INetworkedComponent>> _factories = new();
    
    private static readonly Lazy<NetworkedComponentRegistry> DefaultInstance = new(CreateDefault);

    /// <summary>
    ///  The default networked component registry.
    /// </summary>
    public static NetworkedComponentRegistry Default => DefaultInstance.Value;

    /// <summary>
    ///  Creates the default networked component registry.
    /// </summary>
    /// <returns></returns>
    private static NetworkedComponentRegistry CreateDefault() {
        var registry = new NetworkedComponentRegistry();
        registry.Register(NetworkedComponentType.Velocity, static () => new VelocityComponent());
        registry.Register(NetworkedComponentType.Action, static () => new ActionComponent());
        registry.Register(NetworkedComponentType.Health, static () => new HealthComponent());
        registry.Register(NetworkedComponentType.Projectile, static () => new ProjectileComponent());
        registry.Register(NetworkedComponentType.Collision, static () => new CollisionComponent());
        registry.Register(NetworkedComponentType.Direction, static () => new DirectionComponent());
        registry.Register(NetworkedComponentType.Target, static () => new TargetComponent());
        return registry;
    }
    
    /// <summary>
    ///  Registers a networked component type with a factory.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="factory"></param>
    private void Register(NetworkedComponentType type, Func<INetworkedComponent> factory) {
        _factories[type] = factory;
    }

    /// <summary>
    ///  Tries to create a networked component of the given type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    public bool TryCreate(NetworkedComponentType type, [NotNullWhen(true)] out INetworkedComponent? component) {
        if (_factories.TryGetValue(type, out var factory)) {
            component = factory();
            return true;
        }

        component = null!;
        return false;
    }
}
