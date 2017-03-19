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
    private Game GameInstance;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();

      var panel = new StackPanel();
      panel.Children.Add( new Canvas() { Width = 800, Height = 600 } );
      this.Content = panel;

      GameInstance = new LineOfBattle.Game( GameCanvas );

      Timer = new DispatcherTimer();
      Timer.Interval = TimeSpan.FromMilliseconds( 1 );
      Timer.Tick += new EventHandler( MainLoop );
      Timer.Start();
    }

    /// <summary>
    /// DispatcherTimer によって呼び出されるイベントハンドラ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainLoop( object sender, EventArgs e )
    {
      GameInstance.MainLoop();
    }
  }
}
