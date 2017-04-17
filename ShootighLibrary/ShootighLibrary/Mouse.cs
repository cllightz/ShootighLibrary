using System.Numerics;

namespace ShootighLibrary
{
    static class Mouse
    {
        public static bool Left;
        public static bool Right;
        public static float X;
        public static float Y;

        public static bool Any => Left || Right;
        public static Vector2 Position => new Vector2( X, Y );
    }
}
