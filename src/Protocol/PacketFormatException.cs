using System;

namespace Yggdrasilnet.Network.Protocol;

/// <summary>
///  Exception thrown when a packet is formatted incorrectly.
/// </summary>
/// <param name="reason"></param>
public sealed class PacketFormatException(string reason) : Exception(reason);
