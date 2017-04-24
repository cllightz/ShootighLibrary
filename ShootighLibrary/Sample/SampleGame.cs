using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using ShootighLibrary;

namespace Sample
{
    class SampleGame : Game
    {
        #region Fields
        public Ball Ball;
        #endregion

        #region Constructor
        public SampleGame( GameControl control ) : base( control )
            => Control = control;
        #endregion

        #region Abstract methods implementations
        public override void Initialize()
            => Ball = new Ball( new DrawOptions( new Vector2( Width / 2, Height / 2 ), 6, new RawColor4( 0, 1, 0, 1 ) ) );

        public override void MainLoop( RenderTarget target )
        {
            Ball.Move();
            Ball.Draw( target );
        }
        #endregion
    }
}
