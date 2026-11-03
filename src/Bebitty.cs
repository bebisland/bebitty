using System.Text;
using Raylib_cs;
using static Raylib_cs.ConfigFlags;
using static Raylib_cs.Raylib;
using static Util;

internal static class Bebitty
{
    private static void Main()
    {
        SetTraceLogLevel(TraceLogLevel.Error);
        SetConfigFlags(ResizableWindow | TransparentWindow | VSyncHint);
        InitWindow(1000, 600, "Bebitty");
        SetExitKey(KeyboardKey.Null);

        Status.Update();
        Pty.Send("");

        while (!WindowShouldClose())
        {
            Status.Update();

            Input();

            BeginDrawing();
            ClearBackground(new Color(10, 10, 0, 240));

            Terminal.Draw();
            Bebis.Draw();

            EndDrawing();
        }

        Bebis.Dispose();

        CloseWindow();
    }

    private static void Input()
    {
        var ctrl = IsKeyDown(KeyboardKey.LeftControl) || IsKeyDown(KeyboardKey.RightControl);
        var alt  = IsKeyDown(KeyboardKey.LeftAlt);

        int codepoint;

        while ((codepoint = GetCharPressed()) > 0)
        {
            if (codepoint < 32 || codepoint == 127 || !Rune.IsValid(codepoint))
                continue;

            Pty.Send(char.ConvertFromUtf32(codepoint));
        }

        if (Pressed(KeyboardKey.Enter))
            Pty.Send((alt ? "\e" : "") + "\r");

        if (Pressed(KeyboardKey.Backspace))
            Pty.Send(ctrl || alt ? "\e" + "\u007f" : "\u007f");
    }
}