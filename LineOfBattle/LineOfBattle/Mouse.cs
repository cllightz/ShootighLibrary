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
    public static float X;
    public static float Y;

    public static bool Any { get { return Left || Right; } }
  }
}
