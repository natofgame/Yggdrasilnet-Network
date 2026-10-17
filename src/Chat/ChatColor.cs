namespace Yggdrasilnet.Network.Chat;

/// <summary>
///  Chat colors, same palette and order as Minecraft (code <c>~0</c> to <c>~f</c>).
/// </summary>
public enum ChatColor : byte {
    Black,
    DarkBlue,
    DarkGreen,
    DarkAqua,
    DarkRed,
    DarkPurple,
    Gold,
    Gray,
    DarkGray,
    Blue,
    Green,
    Aqua,
    Red,
    LightPurple,
    Yellow,
    White,

    /// <summary>
    ///  No color set: the UI chooses (code <c>~r</c>).
    /// </summary>
    Default
}

/// <summary>
///  Helpers for <see cref="ChatColor" />.
/// </summary>
public static class ChatColorExtensions {
    private static readonly string[] Hex = [
        "#000000", "#0000AA", "#00AA00", "#00AAAA",
        "#AA0000", "#AA00AA", "#FFAA00", "#AAAAAA",
        "#555555", "#5555FF", "#55FF55", "#55FFFF",
        "#FF5555", "#FF55FF", "#FFFF55", "#FFFFFF"
    ];

    /// <summary>
    ///  The hex value of the color (e.g. <c>#FF5555</c>), or null for <see cref="ChatColor.Default" />.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string? ToHex(this ChatColor color) {
        return color == ChatColor.Default ? null : Hex[(int)color];
    }
}
