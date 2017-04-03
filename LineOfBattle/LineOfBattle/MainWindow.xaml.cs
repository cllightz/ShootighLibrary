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

            this.KeyDown += this.KeyDownEventHandler;
            this.KeyUp += this.KeyUpEventHandler;

            this.MouseDown += this.MouseDownEventHandler;
            this.MouseUp += this.MouseUpEventHandler;
            this.MouseMove += this.MouseMoveEventHandler;
        }

        #region Event Handlers
        /// <summary>
        /// キーを押した時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownEventHandler( object sender, KeyEventArgs e )
        {
            switch ( e.Key ) {
                case System.Windows.Input.Key.W:
                    Key.W = true;
                    break;

                case System.Windows.Input.Key.A:
                    Key.A = true;
                    break;

                case System.Windows.Input.Key.S:
                    Key.S = true;
                    break;

                case System.Windows.Input.Key.D:
                    Key.D = true;
                    break;
            }
        }

        /// <summary>
        /// キーを離した時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUpEventHandler( object sender, KeyEventArgs e )
        {
            switch ( e.Key ) {
                case System.Windows.Input.Key.W:
                    Key.W = false;
                    break;

                case System.Windows.Input.Key.A:
                    Key.A = false;
                    break;

                case System.Windows.Input.Key.S:
                    Key.S = false;
                    break;

                case System.Windows.Input.Key.D:
                    Key.D = false;
                    break;
            }
        }

        /// <summary>
        /// マウスボタンを押した時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownEventHandler( object sender, MouseButtonEventArgs e )
        {
            switch ( e.ChangedButton ) {
                case MouseButton.Left:
                    Mouse.Left = true;
                    break;

                case MouseButton.Right:
                    Mouse.Right = true;
                    break;
            }
        }

        /// <summary>
        /// マウスボタンを離した時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpEventHandler( object sender, MouseButtonEventArgs e )
        {
            switch ( e.ChangedButton ) {
                case MouseButton.Left:
                    Mouse.Left = false;
                    break;

                case MouseButton.Right:
                    Mouse.Right = false;
                    break;
            }
        }

        /// <summary>
        /// マウスカーソルを動かした時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMoveEventHandler( object sender, MouseEventArgs e )
        {
            var pos = e.GetPosition( this.gameControl );
            Mouse.X = (float)pos.X;
            Mouse.Y = (float)pos.Y;
        }
        #endregion
    }
}
