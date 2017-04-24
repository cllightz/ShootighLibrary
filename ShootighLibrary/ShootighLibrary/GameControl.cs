using System;
using System.Windows;
using System.Windows.Input;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ShootighLibrary
{
    /// <summary>
    /// ゲームのコントロール
    /// </summary>
    public class GameControl : D2dControl.D2dControl
    {
        #region Fields
        private Game GameInstance;
        private bool IsGameInitialized;
        #endregion

        /// <summary>
        /// コントロールのコンストラクタ。
        /// </summary>
        public GameControl() => IsGameInitialized = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">抽象クラスGameを実装したクラスの実体を渡す。</param>
        public void SetGameInstance( Game game ) => GameInstance = game;

        /// <summary>
        /// ゲームループ
        /// </summary>
        public override void Render( RenderTarget target )
        {
            if ( !IsGameInitialized ) {
                GameInstance.Initialize();
                IsGameInitialized = true;
            }

            target.Clear( new RawColor4( 0, 0, 0, 1 ) );
            GameInstance.MainLoop( target );
            GC.Collect();
        }

        /// <summary>
        /// 引数で渡されたMainWindowの実体の各種イベントハンドラを設定する。
        /// </summary>
        /// <param name="mainwindow"></param>
        public void SetEventHandlers( Window mainwindow )
        {
            mainwindow.KeyDown += KeyDownEventHandler;
            mainwindow.KeyUp += KeyUpEventHandler;

            mainwindow.MouseDown += MouseDownEventHandler;
            mainwindow.MouseUp += MouseUpEventHandler;
            mainwindow.MouseMove += MouseMoveEventHandler;
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
            var pos = e.GetPosition( this );
            Mouse.X = (float)pos.X;
            Mouse.Y = (float)pos.Y;
        }
        #endregion
    }
}
