namespace Yggdrasilnet.Network.Packet;

public enum PacketType : byte {
    PlayerConnexion = 0,
    Input = 1,
    SpawnEntities = 2,
    Stats = 3,
    SnapshotChunk = 4,
    EntityDefinitions = 5,
    CastSpell = 6,
    DespawnEntities = 7
}
