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
        public float Left => Padding;
        public float Right => Width - Padding;
        public float Top => Padding;
        public float Bottom => Height - Padding;
        #endregion

        #region Abstract Methods
        public abstract void Initialize();
        public abstract void MainLoop();
        #endregion
    }
}
