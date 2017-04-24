using System.Numerics;
using SharpDX.Mathematics.Interop;

namespace ShootighLibrary
{
    /// <summary>
    /// IDrawableを実装するクラスに渡す描画設定をまとめたクラス。
    /// 構造体ではないため、同じインスタンスを複数回渡すと不具合の原因となる。
    /// 同じ設定を使い回したいときは、1つのインスタンスの.Clone()を利用してコピーのインスタンスを渡す。
    /// </summary>
    public class DrawOptions
    {
        public Vector2 Position;
        public float Size;
        public RawColor4 Color;

        public DrawOptions( Vector2 position, float size, RawColor4 color )
        {
            Position = position;
            Size = size;
            Color = color;
        }

        public DrawOptions Clone
            => new DrawOptions( Position, Size, Color );
    }
}
