using System;

namespace Yggdrasilnet.NetworkV4.Protocol;

/// <summary>
///  Exception thrown when a packet is formatted incorrectly.
/// </summary>
/// <param name="reason"></param>
public sealed class PacketFormatException(string reason) : Exception(reason);
