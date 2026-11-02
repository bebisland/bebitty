using static Raylib_cs.Raylib;

internal static class Time
{
    public static float Now;

    public static void Update()
    {
        Now = (float)GetTime();
    }
}