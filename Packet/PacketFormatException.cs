using System;

namespace Yggdrasilnet.Network.Packet;

public sealed class PacketFormatException(string message) : Exception(message);