using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LineOfBattle
{
  /// <summary>
  /// ゲームのロジック
  /// </summary>
  class Game
  {
    private Canvas GameCanvas;
    private Random Rand;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="canvas"></param>
    public Game( Canvas canvas )
    {
      GameCanvas = canvas;
      Rand = new Random();
    }

    /// <summary>
    /// ゲームループ
    /// </summary>
    public void MainLoop()
    {
      GameCanvas.Children.Clear();

      var ellipse = new Ellipse();
      ellipse.Fill = new SolidColorBrush( Color.FromRgb( 255, 0, 0 ) );
      ellipse.Width = Rand.Next( 10, 100 );
      ellipse.Height = Rand.Next( 10, 100 );
      Canvas.SetTop( ellipse, Rand.Next( 10, 100 ) );
      Canvas.SetLeft( ellipse, Rand.Next( 10, 100 ) );
      this.GameCanvas.Children.Add( ellipse );
    }
  }
}
