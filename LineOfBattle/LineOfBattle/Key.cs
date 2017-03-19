using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LineOfBattle
{
  static class Key
  {
    public static bool W;
    public static bool A;
    public static bool S;
    public static bool D;

    public static bool AnyDirection { get { return W || A || S || D; } }

    public static Vector Direction {
      get {
        double x = (A ? -1 : 0) + (D ? 1 : 0);
        double y = (W ? -1 : 0) + (S ? 1 : 0);

        return new Vector( x, y );
      }
    }
  }
}
