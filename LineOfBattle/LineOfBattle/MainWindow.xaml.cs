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
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {
    private Canvas GameCanvas;
    private DispatcherTimer Timer;
    private Random Rand;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();

      StackPanel panel = new StackPanel();
      GameCanvas = new Canvas();
      GameCanvas.Width = 800;
      GameCanvas.Height = 600;

      panel.Children.Add( GameCanvas );
      this.Content = panel;

      Timer = new DispatcherTimer();
      Timer.Interval = TimeSpan.FromMilliseconds( 1 );
      Timer.Tick += new EventHandler( MainLoop );
      Timer.Start();

      Rand = new Random();
    }

    /// <summary>
    /// ゲームループ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainLoop( object sender, EventArgs e )
    {
      GameCanvas.Children.Clear();

      Ellipse ellipse = new Ellipse();
      ellipse.Fill = new SolidColorBrush( Color.FromRgb( 255, 0, 0 ) );
      ellipse.Width = Rand.Next( 10, 100 );
      ellipse.Height = Rand.Next( 10, 100 );
      Canvas.SetTop( ellipse, Rand.Next( 10, 100 ) );
      Canvas.SetLeft( ellipse, Rand.Next( 10, 100 ) );
      this.GameCanvas.Children.Add( ellipse );
    }
  }
}
