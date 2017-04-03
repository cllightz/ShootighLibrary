using System;
using System.Numerics;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    static class VectorExtensions
    {
        public static RawVector2 ToRawVector2( this Vector2 v ) => new RawVector2() { X = v.X, Y = v.Y };
        public static Vector2 ToVector2( this RawVector2 v ) => new Vector2( v.X, v.Y );
        public static Vector2 GetNormalizedVector2( this Vector2 v ) => v / v.Length();
    }
}
