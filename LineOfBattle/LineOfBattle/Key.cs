using System;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    static class Key
    {
        public static bool W;
        public static bool A;
        public static bool S;
        public static bool D;

        public static bool AnyDirection => W || A || S || D;

        public static RawVector2 Direction
        {
            get {
                float x = (A ? -1 : 0) + (D ? 1 : 0);
                float y = (W ? -1 : 0) + (S ? 1 : 0);
                var norm = (float)Math.Sqrt( x * x + y * y );
                x = (norm == 0) ? 0 : x / norm;
                y = (norm == 0) ? 0 : y / norm;

                return new RawVector2() { X = x, Y = y };
            }
        }
    }
}
