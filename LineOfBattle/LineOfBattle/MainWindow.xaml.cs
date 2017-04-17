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
            InitializeComponent();
            GameControl.SetGameInstance( new LoB() );
            GameControl.SetEventHandlers( this );
        }
    }
}
