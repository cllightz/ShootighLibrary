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
            Units = new List<Unit>();
            UnitAdditionQueue = new Queue<Unit>();
        }

        public void Add( Unit u )
            => UnitAdditionQueue.Enqueue( u );

        public void Move()
        {
            if ( Units.Any() && Key.AnyDirection && CanMove ) {
                if ( Key.Shift ) {
                    foreach ( var u in Units ) {
                        u.MoveV( Speed * Key.Direction, Maneuver.Simultaneously );
                    }
                } else {
                    Units[ 0 ].MoveV( GetCorrectedDirection( Units[ 0 ] ), Maneuver.Successively );

                    for ( var i = 1; i < Units.Count; i++ ) {
                        if ( Units[ i - 1 ].HasFollowPos ) {
                            Units[ i ].Move( Units[ i - 1 ].GetFollowPos() );
                        }
                    }
                }
            }

            if ( (!Units.Any() || Units.Last().HasFollowPos) && UnitAdditionQueue.Any() ) {
                Units.Add( UnitAdditionQueue.Peek() );
                UnitAdditionQueue.Dequeue();
            }
        }

        public bool CanMove
        {
            get {
                (var x, var y) = (Units[ 0 ].DrawOptions.Position + Speed * Key.Direction).Tuple();

                if ( Globals.Game.Left <= x && x <= Globals.Game.Right && Globals.Game.Top <= y && y <= Globals.Game.Bottom ) {
                    return true;
                }

                if ( !(Globals.Game.Left <= x && x <= Globals.Game.Right || Globals.Game.Top <= y && y <= Globals.Game.Bottom) ) {
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
        }

        public Vector2 GetCorrectedDirection( Unit u )
        {
            float to1( float f ) => f < 0 ? -1 : f > 0 ? 1 : 0;

            var newposition = u.DrawOptions.Position + Speed * Key.Direction;
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

        public void Draw()
        {
            foreach ( var u in Units ) {
                u.Draw();
            }
        }

        #region IEnumerableの実装
        public IEnumerator<Unit> GetEnumerator() => Units.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Units.GetEnumerator();
        #endregion
    }
}
