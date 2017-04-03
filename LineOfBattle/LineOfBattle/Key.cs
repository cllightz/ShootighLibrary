﻿using System.Numerics;

namespace LineOfBattle
{
    static class Key
    {
        public static bool W;
        public static bool A;
        public static bool S;
        public static bool D;

        public static bool AnyDirection => W || A || S || D;

        public static Vector2 Direction
            => new Vector2( A ? -1 : D ? 1 : 0, W ? -1 : S ? 1 : 0 ).GetNormalizedVector2();
    }
}
