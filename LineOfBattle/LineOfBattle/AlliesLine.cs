using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LineOfBattle
{
    class AlliesLine
    {
        public List<Unit> Units { get; private set; }
        private Queue<Unit> UnitAdditionQueue;

        public AlliesLine()
        {
            Units = new List<Unit>();
            UnitAdditionQueue = new Queue<Unit>();
        }

        public void Add( Unit u )
        {
            UnitAdditionQueue.Enqueue( u );
        }

        public void Move()
        {
            if ( Units.Any() && Key.AnyDirection ) {
                Units[ 0 ].MoveV( new RawVector2() { X = 2 * Key.Direction.X, Y = 2 * Key.Direction.Y } );

                for ( int i = 1; i < Units.Count; i++ ) {
                    if ( Units[ i - 1 ].HasFollowPos ) {
                        Units[ i ].Move( Units[ i - 1 ].GetFollowPos() );
                    }
                }
            }

            if ( (!Units.Any() || Units.Last().HasFollowPos) && UnitAdditionQueue.Any() ) {
                Units.Add( UnitAdditionQueue.Peek() );
                UnitAdditionQueue.Dequeue();
            }
        }

        public void Draw()
        {
            foreach ( var u in Units ) {
                u.Draw();
            }
        }
    }
}
