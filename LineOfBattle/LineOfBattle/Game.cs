﻿using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.DirectWrite;

namespace LineOfBattle
{
    /// <summary>
    /// ゲームのロジック
    /// </summary>
    class Game : D2dControl.D2dControl
    {
        #region Fields
        public RenderTarget Target;

        public Random Rand = new Random();

        public ScheneState State;
        public AlliesLine Allies;
        public List<Unit> Enemies;
        public List<Shell> AlliesShells;
        public List<Shell> EnemiesShells;
        public ulong FrameCount;
        private bool IsGameInitialized;
        #endregion

        public Game()
        {
            Globals.Game = this;

            this.State = ScheneState.TITLE;

            this.Allies = new AlliesLine();

            this.Enemies = new List<Unit>();
            this.AlliesShells = new List<Shell>();
            this.EnemiesShells = new List<Shell>();
            this.FrameCount = 0;
            this.IsGameInitialized = false;
        }

        /// <summary>
        /// 1回目のゲームループで呼ばれる初期化処理．
        /// RenderTargetの情報を必要とするものを記述する．
        /// </summary>
        public void Initialize()
        {
            var center = new RawVector2() {
                X = (float)this.ActualWidth / 2,
                Y = (float)this.ActualHeight / 2
            };

            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            this.Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );

            this.IsGameInitialized = true;
        }

        /// <summary>
        /// ゲームループ
        /// </summary>
        public override void Render( RenderTarget target )
        {
            if ( !this.IsGameInitialized ) {
                Initialize();
            }

            this.Target = target;
            GC.Collect();
            System.Diagnostics.Debug.WriteLine( this.resCache.Count );
            target.Clear( new RawColor4( 0, 0, 0, 1 ) );

            switch ( this.State ) {
                case ScheneState.TITLE:
                    DrawTitle();
                    break;

                case ScheneState.BATTLE:
                    MoveEnemies();
                    MoveAllies();
                    MoveAlliesShells();
                    MoveEnemiesShells();
                    Shoot();
                    CalculateAlliesShellsCollision();
                    CalculateEnemiesShellsCollision();

                    DrawEnemies();
                    DrawAllies();
                    DrawAlliesShells();
                    DrawEnemiesShells();

                    this.FrameCount++;
                    break;

                case ScheneState.RESULT:
                    break;
            }

            DrawText( this.Fps.ToString(), 12, 10, 01 );
        }

        #region 移動・判定・描画
        private void MoveEnemies()
        {
            foreach ( var u in this.Enemies ) {

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
                    var x = this.AlliesShells[ i ].Position.X;
                    var y = this.AlliesShells[ i ].Position.Y;

                    if ( x < -100 || this.Target.Size.Width + 100 < x || y < -100 || this.Target.Size.Height + 100 < y ) {
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
                var x = this.EnemiesShells[ i ].Position.X;
                var y = this.EnemiesShells[ i ].Position.Y;

                if ( x < -100 || this.Target.Size.Width + 100 < x || y < -100 || this.Target.Size.Height + 100 < y ) {
                    this.EnemiesShells.RemoveAt( i );
                }
            }
        }

        private void Shoot()
        {
            if ( Mouse.Any && this.FrameCount % 10 == 0 ) {
                foreach ( var u in this.Allies.Units ) {
                    var cursor = new RawVector2() { X = Mouse.X, Y = Mouse.Y };
                    var posL = new RawVector2() { X = u.Position.X, Y = u.Position.Y };
                    var posR = new RawVector2() { X = this.Allies.Units.First().Position.X, Y = this.Allies.Units.First().Position.Y };
                    var posLR = new RawVector2() { X = (posL.X + posR.X) / 2, Y = (posL.Y + posR.Y) / 2 };
                    var pos = Mouse.Left ? (Mouse.Right ? posLR : posL) : (Mouse.Right ? posR : new RawVector2() { X = 0, Y = 0 });

                    var direction = new RawVector2() { X = cursor.X - pos.X, Y = cursor.Y - pos.Y };
                    var norm = (float)Math.Sqrt( direction.X * direction.X + direction.Y * direction.Y );
                    direction.X = (norm == 0) ? 0 : direction.X / norm;
                    direction.Y = (norm == 0) ? 0 : direction.Y / norm;

                    var v = new RawVector2() { X = 5 * direction.X, Y = 5 * direction.Y };

                    this.AlliesShells.Add( new Shell( u.Position, v, 5, new RawColor4( 0, 1, 1, 1 ) ) );
                }
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
            DrawText( "Line of Battle", 50, 0, 100 );
            DrawText( "Press Left Mouse Button to Start", 25, 0, 200 );

            if ( Mouse.Left ) {
                this.State = ScheneState.BATTLE;
            }
        }

        /// <summary>
        /// Canvas へのテキストの描画
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawText( string text, float size, float x, float y, TextAlignment alignment = SharpDX.DirectWrite.TextAlignment.Center )
        {
            using ( var format = new TextFormat( new SharpDX.DirectWrite.Factory(), "游ゴシック", size ) { TextAlignment = alignment } )
            using ( var brush = new SolidColorBrush( this.Target, new RawColor4( 1, 1, 1, 1 ) ) ) {
                var rect = new RawRectangleF( x, y, x + this.Target.Size.Width, y + size );
                this.Target.DrawText( text, format, rect, brush );
            }
        }
    }
}
