using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

internal static class Status
{
    // Codepoints
    private static readonly List<int> Codepoints = [];

    // Font
    public static           Font  Font;
    private static readonly int   FontAtlasSize;
    private static readonly float FontBaseAdvance;

    // Screen
    public static int ScreenWidth;
    public static int ScreenHeight;

    public static int Cols;
    public static int Rows;

    // Time
    public static float Time;

    // Pty
    public static bool Exited;
    public static int  ExitSignalled;

    static Status()
    {
        AddCodepoints(32,    126);   // ASCII
        AddCodepoints(160,   383);   // Latin-1, Latin Extended-A
        AddCodepoints(8592,  8703);  // Arrows
        AddCodepoints(8960,  9215);  // Technical Symbols
        AddCodepoints(9472,  9727);  // Box Drawing, Blocks, Geometric Shapes
        AddCodepoints(9728,  10175); // Misc Symbols
        AddCodepoints(10240, 10495); // Braille

        FontAtlasSize   = (int)Config.Terminal.FontSize;
        Font            = LoadFontEx(Config.Terminal.Font, FontAtlasSize, [.. Codepoints], Codepoints.Count);
        FontBaseAdvance = MeasureTextEx(Font, "M", FontAtlasSize, 0).X;
    }

    // Cells
    public static Vector2 CellSize =>
        new(FontBaseAdvance * Config.Terminal.FontSize / FontAtlasSize, MathF.Ceiling(Config.Terminal.FontSize * 1.2f));

    public static void Update()
    {
        ScreenWidth  = GetScreenWidth();
        ScreenHeight = GetScreenHeight();

        Cols = Math.Max(2, (int)((ScreenWidth  - Config.Terminal.PaddingX * 2) / CellSize.X));
        Rows = Math.Max(2, (int)((ScreenHeight - Config.Terminal.PaddingY * 2) / CellSize.Y));

        Time = (float)GetTime();
    }

    private static void AddCodepoints(int first, int last)
    {
        for (var codepoint = first; codepoint <= last; codepoint++)
            Codepoints.Add(codepoint);
    }
}