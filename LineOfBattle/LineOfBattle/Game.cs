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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
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
        #endregion

        public Game()
        {
            Globals.Game = this;

            State = ScheneState.TITLE;

            Allies = new AlliesLine();
            var center = new RawVector2() { X = (float)ActualWidth / 2, Y = (float)ActualHeight / 2 };
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );
            Allies.Add( new Unit( center, 10, new RawColor4( 0, 1, 0, 1 ) ) );

            Enemies = new List<Unit>();
            AlliesShells = new List<Shell>();
            EnemiesShells = new List<Shell>();
            FrameCount = 0;
        }

        /// <summary>
        /// ゲームループ
        /// </summary>
        public override void Render( RenderTarget target )
        {
            Target = target;
            GC.Collect();
            System.Diagnostics.Debug.WriteLine( resCache.Count );
            target.Clear( new RawColor4( 0, 0, 0, 1 ) );

            switch ( State ) {
                case ScheneState.TITLE:
                    DrawTitle();
                    break;

                case ScheneState.BATTLE:
                    MoveEnemies();
                    Allies.Move();
                    MoveAlliesShells();
                    MoveEnemiesShells();
                    Shoot();
                    CalculateAlliesShellsCollision();
                    CalculateEnemiesShellsCollision();

                    DrawEnemies();
                    Allies.Draw();
                    DrawAlliesShells();
                    DrawEnemiesShells();

                    FrameCount++;
                    break;

                case ScheneState.RESULT:
                    break;
            }

            DrawText( Fps.ToString(), 12, 10, 01 );
        }

        private void MoveEnemies()
        {
            foreach ( var u in Enemies ) {

            }
        }

        private void Shoot()
        {
            if ( Mouse.Any && FrameCount % 10 == 0 ) {
                foreach ( var u in Allies.Units ) {
                    var cursor = new RawVector2() { X = Mouse.X, Y = Mouse.Y };
                    var posL = new RawVector2() { X = u.Position.X, Y = u.Position.Y };
                    var posR = new RawVector2() { X = Allies.Units.First().Position.X, Y = Allies.Units.First().Position.Y };
                    var posLR = new RawVector2() { X = (posL.X + posR.X) / 2, Y = (posL.Y + posR.Y) / 2 };
                    var pos = Mouse.Left ? (Mouse.Right ? posLR : posL) : (Mouse.Right ? posR : new RawVector2() { X = 0, Y = 0 });

                    var direction = new RawVector2() { X = cursor.X - pos.X, Y = cursor.Y - pos.Y };
                    var norm = (float)Math.Sqrt( direction.X * direction.X + direction.Y * direction.Y );
                    direction.X = (norm == 0) ? 0 : direction.X / norm;
                    direction.Y = (norm == 0) ? 0 : direction.Y / norm;

                    var v = new RawVector2() { X = 5 * direction.X, Y = 5 * direction.Y };

                    AlliesShells.Add( new Shell( u.Position, v, 5, new RawColor4( 0, 1, 1, 1 ) ) );
                }
            }
        }

        private void MoveAlliesShells()
        {
            foreach ( var s in AlliesShells ) {
                s.Move();
            }

            if ( AlliesShells.Any() ) {
                for ( int i = AlliesShells.Count - 1; 0 <= i; i-- ) {
                    var x = AlliesShells[ i ].Position.X;
                    var y = AlliesShells[ i ].Position.Y;

                    if ( x < -100 || Target.Size.Width + 100 < x || y < -100 || Target.Size.Height + 100 < y ) {
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

            for ( int i = EnemiesShells.Count - 1; 0 <= i; i++ ) {
                var x = EnemiesShells[ i ].Position.X;
                var y = EnemiesShells[ i ].Position.Y;

                if ( x < -100 || Target.Size.Width + 100 < x || y < -100 || Target.Size.Height + 100 < y ) {
                    EnemiesShells.RemoveAt( i );
                }
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

        /// <summary>
        /// タイトル画面の描画
        /// </summary>
        private void DrawTitle()
        {
            DrawText( "Line of Battle", 50, 0, 100 );
            DrawText( "Press Left Mouse Button to Start", 25, 0, 200 );

            if ( Mouse.Left ) {
                State = ScheneState.BATTLE;
            }
        }

        /// <summary>
        /// Canvas へのテキストの描画
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawText( string text, float size, float x, float y )
        {
            using ( var format = new TextFormat( new SharpDX.DirectWrite.Factory(), "游ゴシック", size ) { TextAlignment = SharpDX.DirectWrite.TextAlignment.Center } )
            using ( var brush = new SolidColorBrush( Target, new RawColor4( 1, 1, 1, 1 ) ) ) {
                var rect = new RawRectangleF( x, y, x + Target.Size.Width, y + size );
                Target.DrawText( text, format, rect, brush );
            }
        }
    }
}
