using System.Collections.Generic;
using System.Text;

namespace Yggdrasilnet.NetworkV4.Chat;

/// <summary>
///  Minecraft-like color codes for chat text: <c>~</c> followed by <c>0-9</c> / <c>a-f</c> sets the color,
///  <c>~r</c> resets it, <c>~~</c> is a literal <c>~</c>. Unknown codes are kept as plain text.
/// </summary>
public static class ChatFormatting {
    /// <summary>
    ///  The character that starts a code.
    /// </summary>
    public const char Prefix = '~';

    /// <summary>
    ///  Max length of a message sent by a player.
    /// </summary>
    public const int MaxMessageLength = 256;

    public const string Black = "~0";
    public const string DarkBlue = "~1";
    public const string DarkGreen = "~2";
    public const string DarkAqua = "~3";
    public const string DarkRed = "~4";
    public const string DarkPurple = "~5";
    public const string Gold = "~6";
    public const string Gray = "~7";
    public const string DarkGray = "~8";
    public const string Blue = "~9";
    public const string Green = "~a";
    public const string Aqua = "~b";
    public const string Red = "~c";
    public const string LightPurple = "~d";
    public const string Yellow = "~e";
    public const string White = "~f";
    public const string Reset = "~r";

    /// <summary>
    ///  Splits a formatted text into colored segments.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static List<ChatSegment> Parse(string? text) {
        var segments = new List<ChatSegment>();
        if (string.IsNullOrEmpty(text)) {
            return segments;
        }

        var current = new StringBuilder();
        var color = ChatColor.Default;

        for (var i = 0; i < text!.Length; i++) {
            var c = text[i];
            if (c != Prefix || i + 1 >= text.Length) {
                current.Append(c);
                continue;
            }

            var code = text[i + 1];
            if (code == Prefix) {
                current.Append(Prefix);
                i++;
                continue;
            }

            if (!TryGetColor(code, out var next)) {
                current.Append(c);
                continue;
            }

            Flush(segments, current, color);
            color = next;
            i++;
        }

        Flush(segments, current, color);
        return segments;
    }

    /// <summary>
    ///  Removes every code and returns the plain text (for logs, console...).
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string StripCodes(string? text) {
        var builder = new StringBuilder();
        foreach (var segment in Parse(text)) {
            builder.Append(segment.Text);
        }

        return builder.ToString();
    }

    /// <summary>
    ///  Escapes the codes so the text is displayed exactly as typed (used for player input).
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Escape(string text) {
        return text.Replace("~", "~~");
    }

    /// <summary>
    ///  Resolves the color of a code character.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool TryGetColor(char code, out ChatColor color) {
        code = char.ToLowerInvariant(code);

        if (code is >= '0' and <= '9') {
            color = (ChatColor)(code - '0');
            return true;
        }

        if (code is >= 'a' and <= 'f') {
            color = (ChatColor)(code - 'a' + 10);
            return true;
        }

        color = ChatColor.Default;
        return code == 'r';
    }

    private static void Flush(List<ChatSegment> segments, StringBuilder current, ChatColor color) {
        if (current.Length == 0) {
            return;
        }

        segments.Add(new ChatSegment(current.ToString(), color));
        current.Clear();
    }
}
