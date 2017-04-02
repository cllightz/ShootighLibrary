using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Unit
    {
        public RawVector2 Position;
        private List<RawVector2> History;
        private const int HistoryLength = 20;
        public float Size;
        public RawColor4 Color;

        public Unit( RawVector2 pos, float size, RawColor4 color )
        {
            this.Position = pos;
            this.History = new List<RawVector2>();
            this.Size = size;
            this.Color = color;
        }

        public void Move( RawVector2 newpos )
        {
            this.History.Add( new RawVector2() { X = this.Position.X, Y = this.Position.Y } );
            this.Position = newpos;
        }

        public void MoveV( RawVector2 v )
        {
            this.History.Add( new RawVector2() { X = this.Position.X, Y = this.Position.Y } );
            this.Position.X += v.X;
            this.Position.Y += v.Y;
        }

        public bool HasFollowPos { get { return this.History.Count >= HistoryLength; } }

        public RawVector2 GetFollowPos()
        {
            var res = this.History.First();
            this.History.RemoveAt( 0 );
            return res;
        }

        public void Draw()
        {
            Globals.Game.Target.DrawEllipse( new Ellipse( this.Position, this.Size, this.Size ), new SolidColorBrush( Globals.Game.Target, this.Color ) );
        }
    }
}
