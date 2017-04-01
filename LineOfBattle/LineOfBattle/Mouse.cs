using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineOfBattle
{
  static class Mouse
  {
    public static bool Left;
    public static bool Right;

    public static double X
    {
      get {
        var sp = System.Windows.Forms.Cursor.Position;
        var cp = Game.Canvas.PointToScreen( new System.Windows.Point( sp.X, sp.Y ) );
        return cp.X;
      }

      set {
        X = value;
      }
    }

    public static double Y
    {
      get {
        var sp = System.Windows.Forms.Cursor.Position;
        var cp = Game.Canvas.PointToScreen( new System.Windows.Point( sp.X, sp.Y ) );
        return cp.Y;
      }

      set {
        Y = value;
      }
    }

    public static bool Any { get { return Left || Right; } }
  }
}
