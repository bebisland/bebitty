using System.Numerics;
using Raylib_cs;

internal class Bebis : IDisposable
{
    private readonly Texture2D _tex = Raylib.LoadTexture("assets/bebis.png");

    public void Dispose()
    {
        Raylib.UnloadTexture(_tex);
    }

    public void Draw()
    {
        var ascend = 0.5f;

        const float margin = 16f;
        const float height = 140f;

        var width = height * _tex.Width / _tex.Height;
        var x     = Screen.Width  - width - margin;
        var top   = Screen.Height - ascend * height;

        var src    = new Rectangle(0,              0,            _tex.Width, _tex.Height);
        var dest   = new Rectangle(x + width / 2f, top + height, width,      height);
        var origin = new Vector2(dest.Width / 2f, dest.Height);

        Raylib.DrawTexturePro(_tex, src, dest, origin, 0, Color.White);
    }
}