using Raylib_cs;

internal static class Util
{
    public static Color Rgb(int color, byte alpha = 255)
    {
        return new Color((byte)(color >> 16), (byte)(color >> 8), (byte)color, alpha);
    }

    public static (int X, int Y) GetCellBounds(int column, int cellWidth, int visibleColumns, float gridCellWidth)
    {
        var span  = cellWidth == 2 && column + 1 < visibleColumns ? 2 : 1;
        var left  = (int)MathF.Round(Config.Terminal.PaddingX + column          * gridCellWidth);
        var right = (int)MathF.Round(Config.Terminal.PaddingX + (column + span) * gridCellWidth);
        return new(left, right - left);
    }
}