namespace LineOfBattle
{
    abstract class Game
    {
        #region Fields;
        public ScheneState ScheneState;
        #endregion

        #region Properties
        public float Width => (float)Globals.Control.ActualWidth;
        public float Height => (float)Globals.Control.ActualHeight;
        public float Padding => 10;
        public float Left => this.Padding;
        public float Right => this.Width - this.Padding;
        public float Top => this.Padding;
        public float Bottom => this.Height - this.Padding;
        #endregion

        #region Abstract Methods
        public abstract void Initialize();
        public abstract void MainLoop();
        #endregion
    }
}
