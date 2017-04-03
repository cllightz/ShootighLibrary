using System.Collections.Generic;
using System.Linq;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class AlliesLine
    {
        public List<Unit> Units { get; private set; }
        private Queue<Unit> UnitAdditionQueue;

        public AlliesLine()
        {
            this.Units = new List<Unit>();
            this.UnitAdditionQueue = new Queue<Unit>();
        }

        public void Add( Unit u ) => this.UnitAdditionQueue.Enqueue( u );

        public void Move()
        {
            if ( this.Units.Any() && Key.AnyDirection ) {
                this.Units[ 0 ].MoveV( new RawVector2() { X = 2 * Key.Direction.X, Y = 2 * Key.Direction.Y } );

                for ( var i = 1; i < this.Units.Count; i++ ) {
                    if ( this.Units[ i - 1 ].HasFollowPos ) {
                        this.Units[ i ].Move( this.Units[ i - 1 ].GetFollowPos() );
                    }
                }
            }

            if ( (!this.Units.Any() || this.Units.Last().HasFollowPos) && this.UnitAdditionQueue.Any() ) {
                this.Units.Add( this.UnitAdditionQueue.Peek() );
                this.UnitAdditionQueue.Dequeue();
            }
        }

        public void Draw()
        {
            foreach ( var u in this.Units ) {
                u.Draw();
            }
        }
    }
}
