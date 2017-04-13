using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Unit : IDrawable
    {
        public DrawOptions DrawOptions { get; set; }
        private const int HistoryLength = 20;
        private List<Vector2> History;
        public float RoundsPerSecond;
        public float CoolDownTimer;
        public Func<Vector2, Vector2> MotionRule;
        private Faction Faction;

        #region Constructors
        /// <summary>
        /// Constructor of ally unit.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="roundspersecond"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public Unit( DrawOptions drawoptions, float roundspersecond )
        {
            this.DrawOptions = drawoptions;
            this.History = new List<Vector2>();
            this.RoundsPerSecond = roundspersecond;
            this.CoolDownTimer = 0;
            this.Faction = Faction.Ally;
        }

        /// <summary>
        /// Constructor of enemy unit.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="roundspersecond"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="motionrule"></param>
        public Unit( DrawOptions drawoptions, float roundspersecond, Func<Vector2, Vector2> motionrule )
        {
            this.DrawOptions = drawoptions;
            this.History = new List<Vector2>();
            this.RoundsPerSecond = roundspersecond;
            this.CoolDownTimer = 0;
            this.MotionRule = motionrule;
            this.Faction = Faction.Enemy;
        }
        #endregion

        public bool HasFollowPos => this.History.Count >= HistoryLength;

        #region Move Methods
        public void Move()
        {
        }

        public void Move( Vector2 newposition )
        {
            this.History.Add( this.DrawOptions.Position );
            this.DrawOptions.Position = newposition;
        }

        public void MoveV( Vector2 v, Maneuver maneuver )
        {
            switch ( maneuver ) {
                case Maneuver.Successively:
                    this.History.Add( this.DrawOptions.Position );
                    this.DrawOptions.Position += v;
                    break;

                case Maneuver.Simultaneously:
                    this.DrawOptions.Position += v;

                    for ( var i = 0; i < this.History.Count; i++ ) {
                        this.History[i] += v;
                    }

                    break;
            }
        }
        #endregion 

        public void Shoot()
        {
            if ( Mouse.Any && this.CoolDownTimer <= 0 ) {
                switch ( this.Faction ) {
                    case Faction.Ally:
                        var cursor = Mouse.Position;
                        var posL = this.DrawOptions.Position;
                        var posR = Globals.Game.Allies.Units.First().DrawOptions.Position;
                        var posLR = (posL + posR) / 2;
                        var pos = Mouse.Left ? (Mouse.Right ? posLR : posL) : (Mouse.Right ? posR : throw new InvalidOperationException());
                        var direction = (cursor - pos).GetNormalizedVector2();
                        var velocity = 5 * direction;
                        var drawoptions = new DrawOptions( this.DrawOptions.Position, 5, new RawColor4( 0, 1, 1, 1 ) );
                        Globals.Game.AlliesShells.Add( new Shell( drawoptions, velocity ) );
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
            var ellipse = new Ellipse( this.DrawOptions.Position.ToRawVector2(), this.DrawOptions.Size, this.DrawOptions.Size );
            var brush = new SolidColorBrush( Globals.Game.Target, this.DrawOptions.Color );
            Globals.Game.Target.DrawEllipse( ellipse, brush );
        }
    }
}
