using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    class Label : IDrawable
    {
        public string Text;
        public TextAlignment TextAlignment;

        public Label( DrawOptions drawoptions, string text, TextAlignment textalignment = TextAlignment.Center )
        {
            DrawOptions = drawoptions;
            Text = text;
            TextAlignment = textalignment;
        }

        public DrawOptions DrawOptions { get; set; }

        /// <summary>
        /// Canvas へのテキストの描画
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw()
        {
            using ( var format = new TextFormat( new SharpDX.DirectWrite.Factory(), "游ゴシック", DrawOptions.Size ) { TextAlignment = TextAlignment } )
            using ( var brush = new SolidColorBrush( Globals.Target, DrawOptions.Color ) ) {
                var rect = new RawRectangleF(
                    DrawOptions.Position.X,
                    DrawOptions.Position.Y,
                    DrawOptions.Position.X + Globals.Target.Size.Width,
                    DrawOptions.Position.Y + DrawOptions.Size
                    );

                Globals.Target.DrawText( Text, format, rect, brush );
            }
        }

        public void Move() { }
    }
}
