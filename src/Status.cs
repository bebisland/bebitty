using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

internal static class Status
{
    public static           Font  Font;
    private static readonly int   FontAtlasSize;
    private static readonly float FontBaseAdvance;

    public static int ScreenWidth;
    public static int ScreenHeight;

    public static float Time;

    public static bool Exited;
    public static int  ExitSignalled;

    public static int Cols;
    public static int Rows;
    public static int Scroll;

    public static Vector2 CellSize;

    static Status()
    {
        FontAtlasSize = (int)Config.Terminal.FontSize;
        Font = LoadFontEx(Config.Terminal.Font, FontAtlasSize, [.. KeyInfo.Codepoints], KeyInfo.Codepoints.Count);
        FontBaseAdvance = MeasureTextEx(Font, "M", FontAtlasSize, 0).X;
    }

    public static void LimitScroll()
    {
        Scroll = Math.Clamp(Scroll, 0, Math.Max(0, Terminal.XTerm.Buffer.YBase));
    }

    public static void Update()
    {
        ScreenWidth  = GetScreenWidth();
        ScreenHeight = GetScreenHeight();

        CellSize = new
        (
            FontBaseAdvance * Config.Terminal.FontSize / FontAtlasSize,
            MathF.Ceiling(Config.Terminal.FontSize * 1.2f)
        );

        Cols = Math.Max(2, (int)((ScreenWidth  - Config.Terminal.PaddingX * 2) / CellSize.X));
        Rows = Math.Max(2, (int)((ScreenHeight - Config.Terminal.PaddingY * 2) / CellSize.Y));

        Time = (float)GetTime();
    }
}