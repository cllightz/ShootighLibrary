using System.Numerics;
using SharpDX.Direct2D1;

namespace LineOfBattle
{
    class Shell : IDrawable
    {
        public Vector2 V;

        public Shell( DrawOptions drawoptions, Vector2 v )
        {
            this.DrawOptions = drawoptions;
            this.V = v;
        }

        public DrawOptions DrawOptions { get; set; }

        public void Move() => this.DrawOptions.Position += this.V;

        public void Draw()
        {
            var ellipse = new Ellipse( this.DrawOptions.Position.ToRawVector2(), this.DrawOptions.Size, this.DrawOptions.Size );
            var brush = new SolidColorBrush( Globals.Target, this.DrawOptions.Color );
            Globals.Target.DrawEllipse( ellipse, brush );
        }
    }
}
