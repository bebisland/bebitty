using Raylib_cs;

internal static class Bebitty
{
    private static readonly Bebis Bebis = new();

    private static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.TransparentWindow | ConfigFlags.VSyncHint);
        Raylib.InitWindow(1000, 600, "Bebitty");
        Raylib.SetExitKey(KeyboardKey.Null);

        Init();

        while (!Raylib.WindowShouldClose())
        {
            Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(10, 10, 0, 240));

            Draw();

            Raylib.EndDrawing();
        }

        Close();

        Raylib.CloseWindow();
    }

    private static void Init() { }

    private static void Update()
    {
        Screen.Update();
    }

    private static void Draw()
    {
        Bebis.Draw();
    }

    private static void Close()
    {
        Bebis.Dispose();
    }
}