using System.Numerics;
using Raylib_cs;
using XTerm.Buffer;
using XTerm.Options;
using static Raylib_cs.Raylib;
using static Util;

internal static class Terminal
{
    public static readonly XTerm.Terminal XTerm;

    public static Action<string>? Reply;

    static Terminal()
    {
        XTerm = new XTerm.Terminal(new TerminalOptions { Cols = Status.Cols, Rows = Status.Rows });

        XTerm.DataReceived += (_, args) => Reply?.Invoke(args.Data);
    }

    private static BufferLine? GetLine(int absoluteLine)
    {
        if (absoluteLine < 0 || absoluteLine >= XTerm.Buffer.YBase + XTerm.Rows)
            return null;

        return XTerm.Buffer.GetLine(absoluteLine);
    }

    public static void Draw()
    {
        var cellHeightPx = (int)Status.CellSize.Y;

        for (var row = 0; row < XTerm.Rows; row++)
        {
            var line = GetLine(row);
            if (line == null)
                continue;

            var cellTop = (int)(Config.Terminal.PaddingY + row * Status.CellSize.Y);

            for (var column = 0; column < XTerm.Cols && column < line.Length; column++)
            {
                var cell = line[column];

                if (cell.Width == 0)
                    continue;

                var bounds = GetCellBounds(column, cell.Width, XTerm.Cols, Status.CellSize.X);

                var baseColor = Rgb(Config.Colors.Foreground, cell.Attributes.IsDim() ? (byte)140 : (byte)255);

                DrawCell(cell, bounds.X, cellTop, cellHeightPx, Config.Terminal.FontSize, baseColor);
            }
        }
    }

    private static void DrawCell(in BufferCell cell, int left, int top, int height, float fontSize, Color color)
    {
        var textPosition = new Vector2(left, top + (height - fontSize) / 2f);
        DrawTextCodepoint(Status.Font, cell.CodePoint, textPosition, fontSize, color);
    }
}