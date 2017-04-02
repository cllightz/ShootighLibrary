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
using LineOfBattle;

namespace LineOfBattle
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // private DispatcherTimer Timer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            KeyDown += KeyDownEventHandler;
            KeyUp += KeyUpEventHandler;

            MouseDown += MouseDownEventHandler;
            MouseUp += MouseUpEventHandler;
            MouseMove += MouseMoveEventHandler;
        }

        /// <summary>
        /// DispatcherTimer によって呼び出されるイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainLoop(object sender, EventArgs e)
        {
            // Game.MainLoop();
        }

        /// <summary>
        /// キーを押した時のイベントを処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
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
        private void KeyUpEventHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
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
        private void MouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton) {
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
        private void MouseUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton) {
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
        private void MouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition( gameControl );
            Mouse.X = (float)pos.X;
            Mouse.Y = (float)pos.Y;
        }
    }
}
