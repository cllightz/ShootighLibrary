using SharpDX.Direct2D1;

namespace ShootighLibrary
{
    public abstract class Game
    {
        #region Fields;
        public GameControl Control;
        #endregion

        #region Constuctor
        public Game( GameControl control ) => Control = control;
        #endregion

        #region Properties
        public float Width => (float)Control.ActualWidth;
        public float Height => (float)Control.ActualHeight;
        public float Padding => 10;
        public float Left => Padding;
        public float Right => Width - Padding;
        public float Top => Padding;
        public float Bottom => Height - Padding;
        #endregion

        #region Abstract Methods
        public abstract void Initialize();
        public abstract void MainLoop( RenderTarget target );
        #endregion
    }
}
