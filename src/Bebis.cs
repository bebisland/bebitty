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
        Raylib.DrawTexturePro
        (
            _tex,
            new Rectangle(0, 0, _tex.Width, _tex.Height),
            new Rectangle(0, 0, 32,         32),
            new Vector2(0, 0),
            0,
            Color.White
        );
    }
}