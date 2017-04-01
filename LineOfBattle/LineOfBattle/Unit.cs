using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineOfBattle
{
  class Unit
  {
    public Vector Position;
    private List<Vector> History;
    private int HistoryLength = 20;
    public double Size;
    public SolidColorBrush Brush;

    public Unit( Vector pos, double size, SolidColorBrush brush )
    {
      Position = pos;
      History = new List<Vector>();
      Size = size;
      Brush = brush;
    }

    public void Move( Vector newPos )
    {
      History.Add( new Vector( Position.X, Position.Y ) );
      Position = new Vector( newPos.X, newPos.Y );
    }

    public void MoveV( Vector v )
    {
      History.Add( new Vector( Position.X, Position.Y ) );
      Position = new Vector( Position.X + v.X, Position.Y + v.Y );
    }

    public bool HasFollowPos { get { return History.Count >= HistoryLength; } }

    public Vector GetFollowPos()
    {
      Vector res = History.First();
      History.RemoveAt( 0 );
      return res;
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
