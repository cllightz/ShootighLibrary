using SharpDX.Direct2D1;

namespace LineOfBattle
{
    abstract class Globals<T>
    {
        public static GameControl Control;
        public static RenderTarget Target;
        public static T Game;
    }
}
