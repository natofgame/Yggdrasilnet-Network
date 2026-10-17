using Yggdrasilnet.Network.Protocol.V1.Serializer;

namespace Yggdrasilnet.Network.Protocol.V1;

/// <summary>
///  Protocol V1.
/// </summary>
public static class ProtocolV1 {
    public const int Version = 1;

    /// <summary>
    ///  The protocol.
    /// </summary>
    public static readonly GameProtocol Protocol = new GameProtocol.Builder()
        .SetProtocolVersion(Version)
        .RegisterPacket(0x00, new RequestProtocolSerializerV1())
        .RegisterPacket(0x01, new InputSerializerV1())
        .RegisterPacket(0x02, new CastSpellSerializerV1())
        .RegisterPacket(0x03, new LoginSerializerV1())
        .RegisterPacket(0x04, new CommandSuggestionRequestSerializerV1())
        .RegisterPacket(0x80, new ProtocolAcceptedSerializerV1())
        .RegisterPacket(0x81, new PlayerConnexionSerializerV1())
        .RegisterPacket(0x82, new EntityDefinitionsSerializerV1())
        .RegisterPacket(0x83, new SnapshotChunkSerializerV1())
        .RegisterPacket(0x84, new DespawnEntitiesSerializerV1())
        .RegisterPacket(0x85, new StatsSerializerV1())
        .RegisterPacket(0x86, new SpellbookStateSerializerV1())
        .RegisterPacket(0x87, new CommandSerializerV1())
        .RegisterPacket(0x88, new ChatPacketSerializerV1())
        .RegisterPacket(0x89, new LoginResultSerializerV1())
        .RegisterPacket(0x8A, new CommandSuggestionResponseSerializerV1())
        .Build();
}
