using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public LoB()
            => Rand = new Random();
        #endregion
        
        /// <summary>
        /// 1回目のゲームループで呼ばれる初期化処理．
        /// RenderTargetの情報を必要とするものを記述する．
        /// </summary>
        public override void Initialize()
        {
            Globals.Game = this;

            ScheneState = ScheneState.Title;

            Enemies = new List<Unit>();
            AlliesShells = new List<Shell>();
            EnemiesShells = new List<Shell>();
            FrameCount = 0;

            var drawoptions = new DrawOptions( new Vector2( Width / 2, Height / 2 ), 6, new RawColor4( 0, 1, 0, 1 ) );

            Allies = new AlliesLine() {
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
            switch ( ScheneState ) {
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
            if ( FrameCount % 100 == 0 ) {
                var theta = 2 * Math.PI * Rand.NextDouble();

                Enemies.Add(
                    new Unit(
                        new DrawOptions(
                            new Vector2( Width / 2, Height / 2 ),
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

            FrameCount++;
        }

        #region 移動・判定・描画
        private void MoveEnemies()
        {
            foreach ( var u in Enemies ) {
                u.Move();
            }
        }

        private void MoveAllies()
            => Allies.Move();

        private void MoveAlliesShells()
        {
            foreach ( var s in AlliesShells ) {
                s.Move();
            }

            if ( AlliesShells.Any() ) {
                for ( var i = AlliesShells.Count - 1; 0 <= i; i-- ) {
                    var x = AlliesShells[ i ].DrawOptions.Position.X;
                    var y = AlliesShells[ i ].DrawOptions.Position.Y;

                    if ( x < -100 || Globals.Target.Size.Width + 100 < x || y < -100 || Globals.Target.Size.Height + 100 < y ) {
                        AlliesShells.RemoveAt( i );
                    }
                }
            }
        }

        private void MoveEnemiesShells()
        {
            foreach ( var s in EnemiesShells ) {
                s.Move();
            }

            for ( var i = EnemiesShells.Count - 1; 0 <= i; i++ ) {
                var x = EnemiesShells[ i ].DrawOptions.Position.X;
                var y = EnemiesShells[ i ].DrawOptions.Position.Y;

                if ( x < -100 || Globals.Target.Size.Width + 100 < x || y < -100 || Globals.Target.Size.Height + 100 < y ) {
                    EnemiesShells.RemoveAt( i );
                }
            }
        }

        private void ShootAlliesShells()
        {
            foreach ( var u in Allies.Units ) {
                u.Shoot();
            }
        }

        private void ShootEnemiesShells()
        {
            foreach ( var u in Enemies ) {
                u.Shoot();
            }
        }

        private void CalculateAlliesShellsCollision() { }

        private void CalculateEnemiesShellsCollision() { }

        private void DrawEnemies()
        {
            foreach ( var u in Enemies ) {
                u.Draw();
            }
        }

        private void DrawAllies()
            => Allies.Draw();

        private void DrawAlliesShells()
        {
            foreach ( var s in AlliesShells ) {
                s.Draw();
            }
        }

        private void DrawEnemiesShells()
        {
            foreach ( var s in EnemiesShells ) {
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
                ScheneState = ScheneState.Battle;
            }
        }
    }
}
