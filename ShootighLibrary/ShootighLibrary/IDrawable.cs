using SharpDX.Direct2D1;

namespace ShootighLibrary
{
    public interface IDrawable
    {
        DrawOptions DrawOptions { get; set; }
        void Move();
        void Draw( RenderTarget target );
    }
}
