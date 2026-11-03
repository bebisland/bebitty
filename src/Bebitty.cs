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

            if (Status.Cols != Terminal.XTerm.Cols || Status.Rows != Terminal.XTerm.Rows)
            {
                Terminal.XTerm.Resize(Status.Cols, Status.Rows);
                Pty.Connection?.Resize(Terminal.XTerm.Cols, Terminal.XTerm.Rows);
            }

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
        // Mods
        var ctrl  = IsKeyDown(KeyboardKey.LeftControl) || IsKeyDown(KeyboardKey.RightControl);
        var shift = IsKeyDown(KeyboardKey.LeftShift)   || IsKeyDown(KeyboardKey.RightShift);
        var alt   = IsKeyDown(KeyboardKey.LeftAlt);

        var modifier = 1 + (shift ? 1 : 0) + (alt ? 2 : 0) + (ctrl ? 4 : 0);

        // Chars
        int codepoint;

        while ((codepoint = GetCharPressed()) > 0)
        {
            if (codepoint < 32 || codepoint == 127 || !Rune.IsValid(codepoint))
                continue;

            Pty.Send(char.ConvertFromUtf32(codepoint));
        }

        // Ctrl + Letter
        if (ctrl && !shift)
        {
            for (var keyCode = (int)KeyboardKey.A; keyCode <= (int)KeyboardKey.Z; keyCode++)
                if (Pressed((KeyboardKey)keyCode))
                    Pty.Send(((char)(keyCode - (int)KeyboardKey.A + 1)).ToString());

            if (Pressed(KeyboardKey.Space))
                Pty.Send("\0");
        }

        // Simple keys
        if (Pressed(KeyboardKey.Enter))
            Pty.Send((alt ? "\e" : "") + "\r");

        if (Pressed(KeyboardKey.Tab))
            Pty.Send(shift ? "\e[Z" : "\t");

        if (Pressed(KeyboardKey.Escape))
            Pty.Send("\e");

        if (Pressed(KeyboardKey.Backspace))
            Pty.Send(ctrl || alt ? "\e" + "\u007f" : "\u007f");

        if (Pressed(KeyboardKey.Delete))
            Pty.Send(ctrl ? "\e" + "d" : modifier > 1 ? $"\e[3;{modifier}~" : "\e[3~");

        // Cursor
        foreach (var (key, letter) in KeyInfo.Cursor)
        {
            if (!Pressed(key))
                continue;

            if (shift && !alt && key is KeyboardKey.Left or KeyboardKey.Right or KeyboardKey.Home or KeyboardKey.End)
                continue;

            if (modifier > 1)
                Pty.Send($"\e[1;{modifier}{letter}");
            else if (Terminal.XTerm.ApplicationCursorKeys)
                Pty.Send($"\eO{letter}");
            else
                Pty.Send($"\e[{letter}");
        }

        // Insert/PgUp/PgDn/F5-F12
        foreach (var (key, number) in KeyInfo.Tilde)
        {
            if (!Pressed(key))
                continue;

            if (shift && key is KeyboardKey.PageUp or KeyboardKey.PageDown)
                continue;

            Pty.Send(modifier > 1 ? $"\e[{number};{modifier}~" : $"\e[{number}~");
        }

        // F1-F4
        foreach (var (key, letter) in KeyInfo.Ss3)
            if (Pressed(key))
                Pty.Send(modifier > 1 ? $"\e[1;{modifier}{letter}" : $"\eO{letter}");
    }
}