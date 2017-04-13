using System.Numerics;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class DrawOptions
    {
        public Vector2 Position;
        public float Size;
        public RawColor4 Color;

        public DrawOptions( Vector2 position, float size, RawColor4 color )
        {
            this.Position = position;
            this.Size = size;
            this.Color = color;
        }

        public DrawOptions Clone => new DrawOptions( this.Position, this.Size, this.Color );
    }
}
