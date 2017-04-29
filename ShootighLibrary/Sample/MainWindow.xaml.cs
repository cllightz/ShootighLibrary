using System.Windows;
using System.Windows.Controls;

namespace Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 注意: MainWindow.xaml にデフォルトで記述されている <Grid> </Grid> は削除しておく。

            // MainWindow の子要素に Grid を追加。
            var MainGrid = new Grid();
            AddChild( MainGrid );

            // Grid の子要素に GameControl のインスタンス MainControl を追加。
            var MainControl = new ShootighLibrary.GameControl();
            MainGrid.Children.Add( MainControl );

            // MainControl に初期化されたゲームのロジックのインスタンスを渡す。
            MainControl.SetGameInstance( new SampleGame( MainControl ) );

            // MainWindow でのイベントを登録する。
            MainControl.SetEventHandlers( this );
        }
    }
}
