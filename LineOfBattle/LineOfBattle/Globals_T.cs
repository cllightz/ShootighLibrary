using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;

namespace LineOfBattle
{
    abstract class Globals<T>
    {
        public static GameControl Control;
        public static RenderTarget Target;
        public static T Game;
    }
}
