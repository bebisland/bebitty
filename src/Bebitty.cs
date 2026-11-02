using Raylib_cs;
using static Raylib_cs.ConfigFlags;
using static Raylib_cs.Raylib;

internal static class Bebitty
{
    private static void Main()
    {
        SetTraceLogLevel(TraceLogLevel.Error);
        SetConfigFlags(ResizableWindow | TransparentWindow | VSyncHint);
        InitWindow(1000, 600, "Bebitty");
        SetExitKey(KeyboardKey.Null);

        Status.AtlasSize   = (int)Config.Terminal.FontSize;
        Status.Font        = LoadFontEx(Config.Terminal.Font, Status.AtlasSize, Codepoints.Array, Codepoints.Count);
        Status.BaseAdvance = MeasureTextEx(Status.Font, "M", Status.AtlasSize, 0).X;

        Terminal.XTerm.Write("Welcome to Bebitty!");

        while (!WindowShouldClose())
        {
            Time.Update();
            Screen.Update();

            BeginDrawing();
            ClearBackground(new Color(10, 10, 0, 240));

            Terminal.Draw();
            Bebis.Draw();

            EndDrawing();
        }

        Bebis.Dispose();

        CloseWindow();
    }
}