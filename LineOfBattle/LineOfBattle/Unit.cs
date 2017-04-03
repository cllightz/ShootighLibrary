using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Unit
    {
        private const int HistoryLength = 20;
        private List<Vector2> History;
        public Vector2 Position;
        public float Size;
        public RawColor4 Color;

        public Unit( Vector2 position, float size, RawColor4 color )
        {
            this.Position = position;
            this.History = new List<Vector2>();
            this.Size = size;
            this.Color = color;
        }

        public bool HasFollowPos => this.History.Count >= HistoryLength;

        public void Move( Vector2 newposition )
        {
            this.History.Add( this.Position );
            this.Position = newposition;
        }

        public void MoveV( Vector2 v )
        {
            this.History.Add( this.Position );
            this.Position += v;
        }

        public Vector2 GetFollowPos()
        {
            var res = this.History.First();
            this.History.RemoveAt( 0 );
            return res;
        }

        public void Draw()
        {
            var ellipse = new Ellipse( this.Position.ToRawVector2(), this.Size, this.Size );
            var brush = new SolidColorBrush( Globals.Game.Target, this.Color );
            Globals.Game.Target.DrawEllipse( ellipse, brush );
        }
    }
}
