using System;
using System.Collections.Generic;
using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets;

/// <summary>
///  Packet for entity definitions.
/// </summary>
public sealed class EntityDefinitionsPacket : IGamePacket {
    /// <summary>
    ///  The entity definitions.
    /// </summary>
    public List<EntityDefinitionEntry> Definitions { get; } = [];
}

/// <summary>
///  An entry in the entity definitions packet.
/// </summary>
public readonly struct EntityDefinitionEntry {
    public EntityDefinitionEntry(byte id, string definitionId) {
        if (string.IsNullOrWhiteSpace(definitionId)) {
            throw new ArgumentException("Definition id cannot be empty.", nameof(definitionId));
        }

        Id = id;
        DefinitionId = definitionId;
    }

    public byte Id { get; }
    public string DefinitionId { get; }
}
