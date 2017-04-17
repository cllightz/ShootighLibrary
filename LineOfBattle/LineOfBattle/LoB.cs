using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineOfBattle
{
    /// <summary>
    /// ゲームのロジック
    /// </summary>
    class LoB : Game
    {
        #region Fields
        public Random Rand;

        public AlliesLine Allies;
        public List<Unit> Enemies;
        public List<Shell> AlliesShells;
        public List<Shell> EnemiesShells;
        public ulong FrameCount;
        #endregion

        #region Constructor
        public LoB() => this.Rand = new Random();
        #endregion
        
        /// <summary>
        /// 1回目のゲームループで呼ばれる初期化処理．
        /// RenderTargetの情報を必要とするものを記述する．
        /// </summary>
        public override void Initialize()
        {
            Globals.Game = this;

            this.ScheneState = ScheneState.Title;

            this.Enemies = new List<Unit>();
            this.AlliesShells = new List<Shell>();
            this.EnemiesShells = new List<Shell>();
            this.FrameCount = 0;

            var drawoptions = new DrawOptions( new Vector2( this.Width / 2, this.Height / 2 ), 6, new RawColor4( 0, 1, 0, 1 ) );

            this.Allies = new AlliesLine() {
                new Unit( drawoptions.Clone, 10 ),
                new Unit( drawoptions.Clone, 10 ),
                new Unit( drawoptions.Clone, 10 ),
                new Unit( drawoptions.Clone, 10 ),
                new Unit( drawoptions.Clone, 10 ),
            };
        }

        /// <summary>
        /// ゲームループ
        /// </summary>
        public override void MainLoop()
        {
            switch ( this.ScheneState ) {
                case ScheneState.Title:
                    DrawTitle();
                    break;

                case ScheneState.Battle:
                    BattleLogic();
                    break;

                case ScheneState.Result:
                    break;
            }
        }

        private void BattleLogic()
        {
            if ( this.FrameCount % 100 == 0 ) {
                var theta = 2 * Math.PI * this.Rand.NextDouble();

                this.Enemies.Add(
                    new Unit(
                        new DrawOptions(
                            new Vector2( this.Width / 2, this.Height / 2 ),
                            5,
                            new RawColor4( 1, 0, 0, 1 )
                            ),
                        1,
                        pos => pos + new Vector2( (float)Math.Cos( theta ), (float)Math.Sin( theta ) )
                        )
                    );
            }

            MoveEnemies();
            MoveAllies();
            MoveAlliesShells();
            MoveEnemiesShells();
            ShootAlliesShells();
            ShootEnemiesShells();
            CalculateAlliesShellsCollision();
            CalculateEnemiesShellsCollision();

            DrawEnemies();
            DrawAllies();
            DrawAlliesShells();
            DrawEnemiesShells();

            this.FrameCount++;
        }

        #region 移動・判定・描画
        private void MoveEnemies()
        {
            foreach ( var u in this.Enemies ) {
                u.Move();
            }
        }

        private void MoveAllies() => this.Allies.Move();

        private void MoveAlliesShells()
        {
            foreach ( var s in this.AlliesShells ) {
                s.Move();
            }

            if ( this.AlliesShells.Any() ) {
                for ( var i = this.AlliesShells.Count - 1; 0 <= i; i-- ) {
                    var x = this.AlliesShells[ i ].DrawOptions.Position.X;
                    var y = this.AlliesShells[ i ].DrawOptions.Position.Y;

                    if ( x < -100 || Globals.Target.Size.Width + 100 < x || y < -100 || Globals.Target.Size.Height + 100 < y ) {
                        this.AlliesShells.RemoveAt( i );
                    }
                }
            }
        }

        private void MoveEnemiesShells()
        {
            foreach ( var s in this.EnemiesShells ) {
                s.Move();
            }

            for ( var i = this.EnemiesShells.Count - 1; 0 <= i; i++ ) {
                var x = this.EnemiesShells[ i ].DrawOptions.Position.X;
                var y = this.EnemiesShells[ i ].DrawOptions.Position.Y;

                if ( x < -100 || Globals.Target.Size.Width + 100 < x || y < -100 || Globals.Target.Size.Height + 100 < y ) {
                    this.EnemiesShells.RemoveAt( i );
                }
            }
        }

        private void ShootAlliesShells()
        {
            foreach ( var u in this.Allies.Units ) {
                u.Shoot();
            }
        }

        private void ShootEnemiesShells()
        {
            foreach ( var u in this.Enemies ) {
                u.Shoot();
            }
        }

        private void CalculateAlliesShellsCollision() { }

        private void CalculateEnemiesShellsCollision() { }

        private void DrawEnemies()
        {
            foreach ( var u in this.Enemies ) {
                u.Draw();
            }
        }

        private void DrawAllies() => this.Allies.Draw();

        private void DrawAlliesShells()
        {
            foreach ( var s in this.AlliesShells ) {
                s.Draw();
            }
        }

        private void DrawEnemiesShells()
        {
            foreach ( var s in this.EnemiesShells ) {
                s.Draw();
            }
        }
        #endregion

        /// <summary>
        /// タイトル画面の描画
        /// </summary>
        private void DrawTitle()
        {
            var white = new RawColor4( 1, 1, 1, 1 );
            new Label( new DrawOptions( new Vector2( 0, 100 ), 50, white ), "Line of Battle" ).Draw();
            new Label( new DrawOptions( new Vector2( 0, 200 ), 25, white ), "Press Left Mouse Button to Start" ).Draw();
            
            if ( Mouse.Left ) {
                this.ScheneState = ScheneState.Battle;
            }
        }
    }
}
