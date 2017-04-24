using SharpDX.Direct2D1;
using ShootighLibrary;

namespace Sample
{
    class Ball : IDrawable
    {
        public Ball( DrawOptions drawoptions )
            => DrawOptions = drawoptions;

        public DrawOptions DrawOptions { get; set; }

        public void Move()
        {
            if ( Key.AnyDirection ) {
                DrawOptions.Position += 5 * Key.Direction;
            }
        }

        public void Draw( RenderTarget target )
        {
            var ellipse = new Ellipse( DrawOptions.Position.ToRawVector2(), DrawOptions.Size, DrawOptions.Size );
            var brush = new SolidColorBrush( target, DrawOptions.Color );
            target.DrawEllipse( ellipse, brush );
        }
    }
}
