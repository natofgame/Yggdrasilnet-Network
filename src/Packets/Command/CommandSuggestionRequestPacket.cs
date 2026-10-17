using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets.Command;

/// <summary>
///  Packet sent by the client to get the completions of the command being typed.
/// </summary>
public sealed class CommandSuggestionRequestPacket : IGamePacket {
    /// <summary>
    ///  Id echoed in the response, so the client can drop outdated answers.
    /// </summary>
    public int RequestId { get; set; }

    /// <summary>
    ///  The text typed so far, with its leading "/".
    /// </summary>
    public string Text { get; set; } = string.Empty;
}
