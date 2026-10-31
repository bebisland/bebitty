using Raylib_cs;

internal static class Bebitty
{
    private static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.TransparentWindow | ConfigFlags.VSyncHint);
        Raylib.InitWindow(1000, 600, "Bebitty");
        Raylib.SetExitKey(KeyboardKey.Null);

        var bebis = new Bebis();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(10, 10, 0, 240));

            bebis.Draw();

            Raylib.EndDrawing();
        }

        bebis.Dispose();

        Raylib.CloseWindow();
    }
}