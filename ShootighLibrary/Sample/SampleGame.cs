namespace Sample
{
    class SampleGame : ShootighLibrary.Game
    {
        public Ball Ball;

        public SampleGame( ShootighLibrary.GameControl control ) : base( control )
            => Control = control;

        public override void Initialize()
            => Ball = new Ball( new ShootighLibrary.DrawOptions( new System.Numerics.Vector2( Width / 2, Height / 2 ), 6, new SharpDX.Mathematics.Interop.RawColor4( 0, 1, 0, 1 ) ) );

        public override void MainLoop( SharpDX.Direct2D1.RenderTarget target )
        {
            Ball.Move();
            Ball.Draw( target );
        }
    }
}
