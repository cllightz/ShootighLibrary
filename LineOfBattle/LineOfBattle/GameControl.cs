using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// ゲームループ
        /// </summary>
        public override void Render( RenderTarget target )
        {
            if ( !this.IsGameInitialized ) {
                Game.Initialize();
                this.IsGameInitialized = true;
            }

            Globals.Target = target;
            target.Clear( new RawColor4( 0, 0, 0, 1 ) );
            Game.MainLoop();
            GC.Collect();
        }
    }
}
