using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Yggdrasilnet.NetworkV4.Protocol.V1;

namespace Yggdrasilnet.NetworkV4.Protocol;

/// <summary>
///  Registry for protocols.
/// </summary>
public static class ProtocolRegistry {
    private static readonly Dictionary<int, GameProtocol> Protocols = new();
    private static bool _registered;

    /// <summary>
    ///  Registers a protocol.
    /// </summary>
    /// <param name="protocol"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Register(GameProtocol protocol) =>  Protocols[protocol.ProtocolVersion] = protocol;

    /// <summary>
    ///  Tries to get a protocol by version.
    /// </summary>
    /// <param name="version"></param>
    /// <param name="protocol"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetProtocol(int version, [NotNullWhen(true)] out GameProtocol? protocol) => Protocols.TryGetValue(version, out protocol);
    
    /// <summary>
    ///  Gets the highest protocol.
    /// </summary>
    /// <returns></returns>
    public static GameProtocol GetHighestProtocol() => Protocols.OrderByDescending(x => x.Key).First().Value;

    /// <summary>
    ///  Registers all protocols.
    /// </summary>
    public static void Registers() {
        if (_registered) {
            return;
        }

        Register(ProtocolV1.Protocol);
        _registered = true;
    }
}
