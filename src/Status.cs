using System.Numerics;
using Raylib_cs;

internal static class Status
{
    // Font
    public static int   AtlasSize;
    public static Font  Font;
    public static float BaseAdvance;

    // Cells
    public static Vector2 CellSize =>
        new(BaseAdvance * Config.Terminal.FontSize / AtlasSize, MathF.Ceiling(Config.Terminal.FontSize * 1.2f));
}