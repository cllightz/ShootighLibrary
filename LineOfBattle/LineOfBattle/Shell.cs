using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LineOfBattle
{
  class Shell
  {
    public RawVector2 Position;
    public RawVector2 V;
    public float Size;
    public RawColor4 Color;

    public Shell( RawVector2 pos, RawVector2 v, float size, RawColor4 color )
    {
      Position = pos;
      V = v;
      Size = size;
      Color = color;
    }

    public void Move()
    {
      Position.X += V.X;
      Position.Y += V.Y;
    }

    public void Draw()
    {
      Globals.Game.Target.DrawEllipse( new Ellipse( Position, Size, Size ), new SolidColorBrush( Globals.Game.Target, Color ) );
    }
  }
}
