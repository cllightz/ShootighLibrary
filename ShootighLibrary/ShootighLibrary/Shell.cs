using System.Numerics;
using SharpDX.Direct2D1;

namespace ShootighLibrary
{
    class Shell : IDrawable
    {
        public Vector2 V;

        public Shell( DrawOptions drawoptions, Vector2 v )
        {
            DrawOptions = drawoptions;
            V = v;
        }

        public DrawOptions DrawOptions { get; set; }

        public void Move()
            => DrawOptions.Position += V;

        public void Draw()
        {
            var ellipse = new Ellipse( DrawOptions.Position.ToRawVector2(), DrawOptions.Size, DrawOptions.Size );
            var brush = new SolidColorBrush( Globals.Target, DrawOptions.Color );
            Globals.Target.DrawEllipse( ellipse, brush );
        }
    }
}
