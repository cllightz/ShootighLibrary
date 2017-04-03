using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Shell
    {
        public RawVector2 Position;
        public RawVector2 V;
        public float Size;
        public RawColor4 Color;

        public Shell( RawVector2 pos, RawVector2 v, float size, RawColor4 color )
        {
            this.Position = pos;
            this.V = v;
            this.Size = size;
            this.Color = color;
        }

        public void Move()
        {
            this.Position.X += this.V.X;
            this.Position.Y += this.V.Y;
        }

        public void Draw()
        {
            Globals.Game.Target.DrawEllipse( new Ellipse( this.Position, this.Size, this.Size ), new SolidColorBrush( Globals.Game.Target, this.Color ) );
        }
    }
}
