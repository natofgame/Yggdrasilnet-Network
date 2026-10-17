namespace Yggdrasilnet.NetworkV4.Chat;

/// <summary>
///  A piece of chat text drawn in a single color.
/// </summary>
/// <param name="Text"></param>
/// <param name="Color"></param>
public readonly record struct ChatSegment(string Text, ChatColor Color);
