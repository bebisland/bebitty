using Raylib_cs;

internal static class KeyInfo
{
    public static readonly List<int> Codepoints = [];

    public static readonly (KeyboardKey Key, char Letter)[] Cursor =
    [
        (KeyboardKey.Up, 'A'),
        (KeyboardKey.Down, 'B'),
        (KeyboardKey.Right, 'C'),
        (KeyboardKey.Left, 'D'),
        (KeyboardKey.Home, 'H'),
        (KeyboardKey.End, 'F')
    ];

    public static readonly (KeyboardKey Key, int Number)[] Tilde =
    [
        (KeyboardKey.Insert, 2),
        (KeyboardKey.PageUp, 5),
        (KeyboardKey.PageDown, 6),
        (KeyboardKey.F5, 15),
        (KeyboardKey.F6, 17),
        (KeyboardKey.F7, 18),
        (KeyboardKey.F8, 19),
        (KeyboardKey.F9, 20),
        (KeyboardKey.F10, 21),
        (KeyboardKey.F11, 23),
        (KeyboardKey.F12, 24)
    ];

    public static readonly (KeyboardKey Key, char Letter)[] Ss3 =
    [
        (KeyboardKey.F1, 'P'), (KeyboardKey.F2, 'Q'), (KeyboardKey.F3, 'R'), (KeyboardKey.F4, 'S')
    ];

    static KeyInfo()
    {
        AddCodepoints(32,    126);   // ASCII
        AddCodepoints(160,   383);   // Latin-1, Latin Extended-A
        AddCodepoints(8592,  8703);  // Arrows
        AddCodepoints(8960,  9215);  // Technical Symbols
        AddCodepoints(9472,  9727);  // Box Drawing, Blocks, Geometric Shapes
        AddCodepoints(9728,  10175); // Misc Symbols
        AddCodepoints(10240, 10495); // Braille
    }

    private static void AddCodepoints(int first, int last)
    {
        for (var codepoint = first; codepoint <= last; codepoint++)
            Codepoints.Add(codepoint);
    }
}