using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    static class Mouse
    {
        public static bool Left;
        public static bool Right;
        public static float X;
        public static float Y;

        public static bool Any => Left || Right;
        public static RawVector2 Position => new RawVector2() { X = X, Y = Y };
    }
}
