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
            this.DrawOptions = drawoptions;
            this.Text = text;
            this.TextAlignment = textalignment;
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
            using ( var format = new TextFormat( new SharpDX.DirectWrite.Factory(), "游ゴシック", this.DrawOptions.Size ) { TextAlignment = this.TextAlignment } )
            using ( var brush = new SolidColorBrush( Globals.Game.Target, this.DrawOptions.Color ) ) {
                var rect = new RawRectangleF(
                    this.DrawOptions.Position.X,
                    this.DrawOptions.Position.Y,
                    this.DrawOptions.Position.X + Globals.Game.Target.Size.Width,
                    this.DrawOptions.Position.Y + this.DrawOptions.Size
                    );

                Globals.Game.Target.DrawText( this.Text, format, rect, brush );
            }
        }

        public void Move() { }
    }
}
