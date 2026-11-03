using System.Numerics;
using Raylib_cs;

internal static class Bebis
{
    private static readonly Texture2D Tex = Raylib.LoadTexture("assets/bebis.png");

    public static void Draw()
    {
        var ascend = 0.6f + MathF.Sin(Status.Time) * 0.1f;

        const float margin = 16f;
        const float height = 140f;

        var width = height * Tex.Width / Tex.Height;
        var x     = Status.ScreenWidth  - width - margin;
        var top   = Status.ScreenHeight - ascend * height;

        var src    = new Rectangle(0,              0,            Tex.Width, Tex.Height);
        var dest   = new Rectangle(x + width / 2f, top + height, width,     height);
        var origin = new Vector2(dest.Width / 2f, dest.Height);

        Raylib.DrawTexturePro(Tex, src, dest, origin, 0, Color.White);
    }

    public static void Dispose()
    {
        Raylib.UnloadTexture(Tex);
    }
}