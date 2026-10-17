using System.Collections.Generic;
using Yggdrasilnet.NetworkV4.Protocol;

namespace Yggdrasilnet.NetworkV4.Packets.Command;

/// <summary>
///  Completions for a <see cref="CommandSuggestionRequestPacket" />.
/// </summary>
public sealed class CommandSuggestionResponsePacket : IGamePacket {
    /// <summary>
    ///  The id of the request this answers.
    /// </summary>
    public int RequestId { get; set; }

    /// <summary>
    ///  Index in the request text where the completed word starts.
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    ///  The possible words, replacing the text from <see cref="Start" />.
    /// </summary>
    public List<string> Suggestions { get; } = [];

    /// <summary>
    ///  The remaining arguments of the command, e.g. <c>&lt;entityType&gt; [x] [y] [z]</c>. Empty when unknown.
    /// </summary>
    public string Usage { get; set; } = string.Empty;
}
