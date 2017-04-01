using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineOfBattle
{
  class Shell
  {
    public Vector Position;
    public Vector V;
    public double Size;
    public SolidColorBrush Brush;

    public Shell( Vector pos, Vector v, double size, SolidColorBrush brush )
    {
      Position = pos;
      V = v;
      Size = size;
      Brush = brush;
    }

    public void Move()
    {
      Position = Vector.Add( Position, V );
    }

    public void Draw()
    {
      var ellipse = new Ellipse() {
        Fill = Brush,
        Width = Size,
        Height = Size,
      };

      Canvas.SetLeft( ellipse, Position.X );
      Canvas.SetTop( ellipse, Position.Y );
      Game.Buffer.Add( ellipse );
    }
  }
}
