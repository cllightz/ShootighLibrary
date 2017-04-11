using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LineOfBattle
{
    class AlliesLine : IEnumerable<Unit>
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
                if ( Key.Shift ) {
                    foreach ( var u in this.Units ) {
                        u.MoveV( 2 * Key.Direction, Maneuver.Simultaneously );
                    }
                } else {
                    this.Units[ 0 ].MoveV( 2 * Key.Direction, Maneuver.Successively );

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

        public void Draw()
        {
            foreach ( var u in this.Units ) {
                u.Draw();
            }
        }

        public IEnumerator<Unit> GetEnumerator() => this.Units.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.Units.GetEnumerator();
    }
}
