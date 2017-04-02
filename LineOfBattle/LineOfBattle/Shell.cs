using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            this.Position.X += V.X;
            this.Position.Y += V.Y;
        }

        public void Draw()
        {
            Globals.Game.Target.DrawEllipse( new Ellipse( this.Position, this.Size, this.Size ), new SolidColorBrush( Globals.Game.Target, this.Color ) );
        }
    }
}
