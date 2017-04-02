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

        public Unit(RawVector2 pos, float size, RawColor4 color)
        {
            Position = pos;
            History = new List<RawVector2>();
            Size = size;
            Color = color;
        }

        public void Move(RawVector2 newpos)
        {
            History.Add( new RawVector2() { X = Position.X, Y = Position.Y } );
            Position = newpos;
        }

        public void MoveV(RawVector2 v)
        {
            History.Add( new RawVector2() { X = Position.X, Y = Position.Y } );
            Position.X += v.X;
            Position.Y += v.Y;
        }

        public bool HasFollowPos { get { return History.Count >= HistoryLength; } }

        public RawVector2 GetFollowPos()
        {
            RawVector2 res = History.First();
            History.RemoveAt( 0 );
            return res;
        }

        public void Draw()
        {
            Globals.Game.Target.DrawEllipse( new Ellipse( Position, Size, Size ), new SolidColorBrush( Globals.Game.Target, Color ) );
        }
    }
}
