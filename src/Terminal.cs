using System.Numerics;
using Raylib_cs;
using XTerm.Buffer;
using XTerm.Common;
using XTerm.Options;
using static Raylib_cs.Raylib;
using static Util;

internal static class Terminal
{
    public static readonly XTerm.Terminal XTerm;

    public static Action<string>? Reply;

    static Terminal()
    {
        XTerm = new XTerm.Terminal
        (
            new TerminalOptions { Cols = Status.Cols, Rows = Status.Rows, Scrollback = Config.Terminal.MaxScrollback }
        );

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
        Status.LimitScroll();

        var first = XTerm.Buffer.YBase - Status.Scroll;

        var cellHeightPx = (int)Status.CellSize.Y;

        DrawChar(first, cellHeightPx, false, true);
        DrawChar(first, cellHeightPx, true,  false);
    }

    private static void DrawChar(int first, int cellHeightPx, bool drawForeground, bool drawBackground)
    {
        for (var row = 0; row < XTerm.Rows; row++)
        {
            var line = GetLine(first + row);

            if (line == null)
                continue;

            var cellTop = (int)(Config.Terminal.PaddingY + row * Status.CellSize.Y);

            for (var column = 0; column < XTerm.Cols && column < line.Length; column++)
            {
                var cell = line[column];

                if (cell.Width == 0)
                    continue;

                ResolveColors
                (
                    XTerm.Colors.Take(),
                    cell,
                    XTerm.Options.DrawBoldTextInBrightColors,
                    out var foreground,
                    out var background
                );

                var bounds = GetCellBounds(column, cell.Width, XTerm.Cols, Status.CellSize.X);

                if (drawBackground && background != -1)
                    DrawRectangle(bounds.X, cellTop, bounds.Y, cellHeightPx, Rgb(background));

                if (!drawForeground)
                    continue;

                var baseColor = Rgb(foreground, cell.Attributes.IsDim() ? (byte)140 : (byte)255);
                DrawCell(cell, bounds.X, cellTop, cellHeightPx, Config.Terminal.FontSize, baseColor);
            }
        }
    }

    private static void DrawCell(in BufferCell cell, int left, int top, int height, float fontSize, Color color)
    {
        var textPosition = new Vector2(left, top + (height - fontSize) / 2f);
        DrawTextCodepoint(Status.Font, cell.CodePoint, textPosition, fontSize, color);
    }

    private static void ResolveColors(ColorSnapshot colors,
                                      in BufferCell cell,
                                      bool          boldBright,
                                      out int       foreground,
                                      out int       background)
    {
        foreground = colors.ResolveForeground(cell.Attributes, boldBright);
        background = colors.ResolveBackground(cell.Attributes) ?? -1;

        if (!cell.Attributes.IsInverse())
            return;

        var resolvedForeground = background == -1 ? Config.Colors.Background : background;
        var resolvedBackground = foreground;

        foreground = resolvedForeground;
        background = resolvedBackground;
    }
}