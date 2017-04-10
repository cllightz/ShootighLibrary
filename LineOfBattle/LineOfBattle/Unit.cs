using System;
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
        public float RoundsPerSecond;
        public float CoolDownTimer;
        public float Size;
        public RawColor4 Color;

        public Unit( Vector2 position, float roundspersecond, float size, RawColor4 color )
        {
            this.Position = position;
            this.History = new List<Vector2>();
            this.RoundsPerSecond = roundspersecond;
            this.CoolDownTimer = 0;
            this.Size = size;
            this.Color = color;
        }

        public bool HasFollowPos => this.History.Count >= HistoryLength;

        public void Move( Vector2 newposition )
        {
            this.History.Add( this.Position );
            this.Position = newposition;
        }

        public void MoveV( Vector2 v, Maneuver maneuver )
        {
            switch ( maneuver ) {
                case Maneuver.Successively:
                    this.History.Add( this.Position );
                    this.Position += v;
                    break;

                case Maneuver.Simultaneously:
                    this.Position += v;

                    for ( var i = 0; i < this.History.Count; i++ ) {
                        this.History[i] += v;
                    }

                    break;
            }
        }

        public void Shoot( Faction faction )
        {
            if ( Mouse.Any && this.CoolDownTimer <= 0 ) {
                switch ( faction ) {
                    case Faction.Ally:
                        var cursor = Mouse.Position;
                        var posL = this.Position;
                        var posR = Globals.Game.Allies.Units.First().Position;
                        var posLR = (posL + posR) / 2;
                        var pos = Mouse.Left ? (Mouse.Right ? posLR : posL) : (Mouse.Right ? posR : throw new InvalidOperationException());
                        var direction = (cursor - pos).GetNormalizedVector2();
                        var velocity = 5 * direction;
                        Globals.Game.AlliesShells.Add( new Shell( this.Position, velocity, 5, new RawColor4( 0, 1, 1, 1 ) ) );
                        break;

                    case Faction.Neutral:
                        break;

                    case Faction.Enemy:
                        break;
                }

                this.CoolDownTimer += 1.0f / this.RoundsPerSecond;
            } else {
                this.CoolDownTimer -= 1.0f / 60.0f;
                this.CoolDownTimer = (this.CoolDownTimer < 0) ? 0 : this.CoolDownTimer;
            }
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
