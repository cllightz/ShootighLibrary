using System;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    /// <summary>
    /// ゲームのコントロール
    /// </summary>
    class GameControl : D2dControl.D2dControl
    {
        #region Fields
        private Game GameInstance;
        private bool IsGameInitialized;
        #endregion

        /// <summary>
        /// コントロールのコンストラクタ。
        /// </summary>
        public GameControl()
        {
            Globals.Control = this;
            this.IsGameInitialized = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public void SetGameInstance( Game gameInstance ) => this.GameInstance = gameInstance;

        /// <summary>
        /// ゲームループ
        /// </summary>
        public override void Render( RenderTarget target )
        {
            if ( !this.IsGameInitialized ) {
                this.GameInstance.Initialize();
                this.IsGameInitialized = true;
            }

            Globals.Target = target;
            target.Clear( new RawColor4( 0, 0, 0, 1 ) );
            this.GameInstance.MainLoop();
            GC.Collect();
        }
    }
}
