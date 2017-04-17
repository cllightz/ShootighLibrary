using System.Windows;
using System.Windows.Input;

namespace LineOfBattle
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.GameControl.SetGameInstance( new LoB() );
            this.GameControl.SetEventHandlers( this );
        }
    }
}
