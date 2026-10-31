using static Raylib_cs.Raylib;

internal static class Screen
{
    public static int Width;
    public static int Height;

    public static void Update()
    {
        Width  = GetScreenWidth();
        Height = GetScreenHeight();
    }
}