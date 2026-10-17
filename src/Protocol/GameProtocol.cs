using System;
using System.Collections.Generic;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Protocol;

/// <summary>
///  Represents a protocol.
/// </summary>
public sealed class GameProtocol {
    private readonly Dictionary<Type, PacketDefinition> _packets = new();
    private readonly Dictionary<int, Type> _packetIds = new();
    private readonly ProtocolHelper _helper;

    /// <summary>
    ///  The version of the protocol.
    /// </summary>
    public int ProtocolVersion { get; }

    private GameProtocol(int version, Dictionary<Type, PacketDefinition> packets, GameProtocol? defaultProtocol) {
        ProtocolVersion = version;
        _helper = new ProtocolHelper(this);

        if (defaultProtocol is not null) {
            foreach (var packet in defaultProtocol._packets) {
                _packets[packet.Key] = packet.Value;
            }
        }

        foreach (var packet in packets) {
            _packets[packet.Key] = packet.Value;
        }

        foreach (var packet in _packets) {
            _packetIds[packet.Value.Id] = packet.Key;
        }
    }

    /// <summary>
    ///  Attempts to encode a packet.
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="writer"></param>
    /// <returns></returns>
    public bool TryEncode(IGamePacket packet, NetDataWriter writer) {
        if (!_packets.TryGetValue(packet.GetType(), out var definition)) {
            return false;
        }

        writer.Put((ushort)definition.Id);
        definition.Serialize(packet, _helper, writer);
        return true;
    }

    /// <summary>
    ///  Attempts to decode a packet.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="packet"></param>
    /// <returns></returns>
    public bool TryDecode(NetDataReader reader, out IGamePacket? packet) {
        packet = null;
        if (reader.AvailableBytes < sizeof(ushort)) {
            return false;
        }

        var id = reader.GetUShort();
        return TryDecode(id, reader, out packet);
    }

    /// <summary>
    ///  Attempts to decode a packet with a specific ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reader"></param>
    /// <param name="packet"></param>
    /// <returns></returns>
    public bool TryDecode(int id, NetDataReader reader, out IGamePacket? packet) {
        packet = null;
        if (!_packetIds.TryGetValue(id, out var packetType)) {
            return false;
        }

        if (Activator.CreateInstance(packetType) is not IGamePacket created) {
            return false;
        }

        var definition = _packets[packetType];
        definition.Deserialize(created, _helper, reader);
        packet = created;
        return true;
    }

    /// <summary>
    ///  A builder for a protocol.
    /// </summary>
    public sealed class Builder {
        private readonly Dictionary<Type, PacketDefinition> _packets = new();
        private GameProtocol? _baseProtocol;
        private int _protocolVersion;

        /// <summary>
        ///  Sets the protocol version.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Builder SetProtocolVersion(int version) {
            if (version <= 0) {
                throw new ArgumentOutOfRangeException(nameof(version));
            }

            _protocolVersion = version;
            return this;
        }

        /// <summary>
        /// Sets the base protocol.
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public Builder SetBase(GameProtocol protocol) {
            _baseProtocol = protocol;
            return this;
        }

        /// <summary>
        ///  Registers a packet.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serializer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Builder RegisterPacket<T>(int id, IPacketSerializer<T> serializer) where T : IGamePacket {
            _packets[typeof(T)] = new PacketDefinition(
                id,
                (packet, helper, writer) => serializer.Serialize((T)packet, helper, writer),
                (packet, helper, reader) => serializer.Deserialize((T)packet, helper, reader)
            );
            return this;
        }

        /// <summary>
        ///  Builds the protocol.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public GameProtocol Build() {
            if (_protocolVersion <= 0) {
                throw new InvalidOperationException("Protocol version must be set.");
            }

            return new GameProtocol(_protocolVersion, _packets, _baseProtocol);
        }
    }
}
