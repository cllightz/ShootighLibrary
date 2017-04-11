using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LineOfBattle
{
    class AlliesLine : IEnumerable<Unit>
    {
        public List<Unit> Units { get; private set; }
        private Queue<Unit> UnitAdditionQueue;
        public const float Speed = 2.0f;

        public AlliesLine()
        {
            this.Units = new List<Unit>();
            this.UnitAdditionQueue = new Queue<Unit>();
        }

        public void Add( Unit u ) => this.UnitAdditionQueue.Enqueue( u );

        public void Move()
        {
            if ( this.Units.Any() && Key.AnyDirection && this.CanMove() ) {
                if ( Key.Shift ) {
                    foreach ( var u in this.Units ) {
                        u.MoveV( Speed * Key.Direction, Maneuver.Simultaneously );
                    }
                } else {
                    this.Units[ 0 ].MoveV( GetCorrectedDirection( this.Units[ 0 ] ), Maneuver.Successively );

                    for ( var i = 1; i < this.Units.Count; i++ ) {
                        if ( this.Units[ i - 1 ].HasFollowPos ) {
                            this.Units[ i ].Move( this.Units[ i - 1 ].GetFollowPos() );
                        }
                    }
                }
            }

            if ( (!this.Units.Any() || this.Units.Last().HasFollowPos) && this.UnitAdditionQueue.Any() ) {
                this.Units.Add( this.UnitAdditionQueue.Peek() );
                this.UnitAdditionQueue.Dequeue();
            }
        }

        public bool CanMove()
        {
            var newposition = this.Units[ 0 ].Position + Speed * Key.Direction;
            var x = newposition.X;
            var y = newposition.Y;

            if ( Globals.Game.Left <= x && x <= Globals.Game.Right && Globals.Game.Top <= y && y <= Globals.Game.Bottom ) {
                return true;
            }

            if ( !( Globals.Game.Left <= x && x <= Globals.Game.Right || Globals.Game.Top <= y && y <= Globals.Game.Bottom) ) {
                return false;
            }

            if ( Key.A && (Key.W || Key.S) && x < Globals.Game.Left ) {
                return true;
            }

            if ( Key.D && (Key.W || Key.S) && Globals.Game.Right < x ) {
                return true;
            }

            if ( Key.W && (Key.A || Key.D) && y < Globals.Game.Top ) {
                return true;
            }

            if ( Key.S && (Key.A || Key.D) && Globals.Game.Left < y ) {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            foreach ( var u in this.Units ) {
                u.Draw();
            }
        }

        public Vector2 GetCorrectedDirection( Unit u )
        {
            float to1( float f ) => f < 0 ? -1 : f > 0 ? 1 : 0;

            var newposition = u.Position + Speed * Key.Direction;
            var x = newposition.X;
            var y = newposition.Y;

            if ( x < Globals.Game.Left || Globals.Game.Right < x ) {
                return new Vector2( 0, Speed * to1( Key.Direction.Y ) );
            }

            if ( y < Globals.Game.Top || Globals.Game.Bottom < y ) {
                return new Vector2( Speed * to1( Key.Direction.X ), 0 );
            }

            return Speed * Key.Direction;
        }

        public IEnumerator<Unit> GetEnumerator() => this.Units.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.Units.GetEnumerator();
    }
}
