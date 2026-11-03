using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Util;

internal static class Cursor
{
    private static bool _showCursor;

    private static int _col = -1;
    private static int _row = -1;

    private static readonly Vector2[] Positions = new Vector2[4];

    public static void Draw()
    {
        Update();

        if (!_showCursor)
            return;

        var cursorColor = Rgb(Config.Colors.Cursor);

        if (IsWindowFocused())
            FillQuad(Positions[0], Positions[1], Positions[2], Positions[3], cursorColor);
        else
            for (var corner = 0; corner < 4; corner++)
                DrawLineEx(Positions[corner], Positions[(corner + 1) % 4], 1.5f, cursorColor);
    }

    private static void FillQuad(Vector2 tl, Vector2 bl, Vector2 br, Vector2 tr, Color color)
    {
        DrawTriangle(tl, bl, br, color);
        DrawTriangle(tl, br, bl, color);
        DrawTriangle(tl, br, tr, color);
        DrawTriangle(tl, tr, br, color);
    }

    private static void Update()
    {
        var col = Math.Min(Terminal.XTerm.Buffer.X, Math.Max(0, Terminal.XTerm.Cols - 1));
        var row = Terminal.XTerm.Buffer.Y + Status.Scroll;

        var x = Config.Terminal.PaddingX + col * Status.CellSize.X;
        var y = Config.Terminal.PaddingY + row * Status.CellSize.Y;

        _showCursor = Terminal.XTerm.CursorVisible && row < Terminal.XTerm.Rows;

        Vector2[] targetCorners =
        [
            new(x, y),
            new(x, y + Status.CellSize.Y),
            new(x    + Status.CellSize.X, y + Status.CellSize.Y),
            new(x    + Status.CellSize.X, y)
        ];

        for (var index = 0; index < 4; index++)
            Positions[index] = targetCorners[index];

        if (col != _col || Terminal.XTerm.Buffer.Y != _row)
        {
            _col = col;
            _row = Terminal.XTerm.Buffer.Y;
        }
    }
}