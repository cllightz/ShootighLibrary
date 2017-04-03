using System.Numerics;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Shell
    {
        public Vector2 Position;
        public Vector2 V;
        public float Size;
        public RawColor4 Color;

        public Shell( Vector2 position, Vector2 v, float size, RawColor4 color )
        {
            this.Position = position;
            this.V = v;
            this.Size = size;
            this.Color = color;
        }

        public void Move() => this.Position += this.V;

        public void Draw()
        {
            var ellipse = new Ellipse( this.Position.ToRawVector2(), this.Size, this.Size );
            var brush = new SolidColorBrush( Globals.Game.Target, this.Color );
            Globals.Game.Target.DrawEllipse( ellipse, brush );
        }
    }
}
