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

namespace LineOfBattle
{
  /// <summary>
  /// ゲームのロジック
  /// </summary>
  class Game : D2dControl.D2dControl
  {
    public static Canvas Canvas;
    public static List<UIElement> Buffer;
    public static Random Rand;

    public static ScheneState State;
    public static AlliesLine Allies;
    public static List<Unit> Enemies;
    public static List<Shell> AlliesShells;
    public static List<Shell> EnemiesShells;
    public static ulong FrameCount;

    private static System.Diagnostics.Stopwatch Watch;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="canvas"></param>
    public static void Initialize( Canvas canvas )
    {
      Canvas = canvas;
      Buffer = new List<UIElement>();
      Rand = new Random();
      State = ScheneState.TITLE;

      Allies = new AlliesLine();
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );
      Allies.Add( new LineOfBattle.Unit( new Vector( Canvas.Width/2, Canvas.Height/2 ), 10, new SolidColorBrush( Color.FromRgb( 0, 255, 0 ) ) ) );

      Enemies = new List<Unit>();
      AlliesShells = new List<Shell>();
      EnemiesShells = new List<Shell>();
      FrameCount = 0;

      Watch = new System.Diagnostics.Stopwatch();
      Watch.Start();
    }

    /// <summary>
    /// ゲームループ
    /// </summary>
    public static void MainLoop()
    {
      Buffer.Clear();

      Buffer.Add( new Rectangle() {
        Width = Canvas.Width,
        Height = Canvas.Height,
        Stroke = Brushes.Black,
        Fill = new SolidColorBrush( Color.FromRgb( 0, 0, 0 ) ),
      } );

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

      Canvas.Children.Clear();

      Watch.Stop();
      // DrawText( $"{1000.0 / Watch.Elapsed.Milliseconds:00.00}", 20, -200, 0 );
      Watch.Restart();

      foreach( var e in Buffer ) {
        Canvas.Children.Add( e );
      }
    }

    private static void MoveEnemies()
    {
      foreach ( var u in Enemies ) {
        
      }
    }

    private static void Shoot()
    {
      if ( Mouse.Any && FrameCount % 10 == 0 ) {
        foreach ( var u in Allies.Units ) {
          var cursor = new Vector( Mouse.X, Mouse.Y );
          var posL = new Vector( u.Position.X, u.Position.Y );
          var posR = new Vector( Allies.Units.First().Position.X, Allies.Units.First().Position.Y );
          var posLR = Vector.Divide( Vector.Add( posL, posR ), 2.0 );
          var pos = Mouse.Left ? (Mouse.Right ? posLR : posL) : (Mouse.Right ? posR : new Vector( 0, 0 ) );

          var direction = Vector.Subtract( cursor, pos );
          direction.Normalize();

          var v = Vector.Multiply( 5.0, direction );

          AlliesShells.Add( new Shell( u.Position, v, 5.0, new SolidColorBrush( Color.FromRgb( 0, 255, 255 ) ) ) );
        }
      }
    }

    private static void MoveAlliesShells()
    {
      foreach ( var s in AlliesShells ) {
        s.Move();
      }

      if ( AlliesShells.Any() ) {
        for ( int i = AlliesShells.Count - 1; 0 <= i; i-- ) {
          var x = AlliesShells[i].Position.X;
          var y = AlliesShells[i].Position.Y;

          if ( x < -100 || Canvas.Width+100 < x || y < -100 || Canvas.Height+100 < y ) {
            AlliesShells.RemoveAt( i );
          }
        }
      }
    }

    private static void MoveEnemiesShells()
    {
      foreach ( var s in EnemiesShells ) {
        s.Move();
      }

      for ( int i = EnemiesShells.Count - 1; 0 <= i; i++ ) {
        var x = EnemiesShells[i].Position.X;
        var y = EnemiesShells[i].Position.Y;

        if ( x < -100 || Canvas.Width+100 < x || y < -100 || Canvas.Height+100 < y ) {
          EnemiesShells.RemoveAt( i );
        }
      }
    }

    private static void CalculateAlliesShellsCollision() { }

    private static void CalculateEnemiesShellsCollision() { }

    private static void DrawEnemies()
    {
      foreach ( var u in Enemies ) {
        u.Draw();
      }
    }

    private static void DrawAlliesShells()
    {
      foreach ( var s in AlliesShells ) {
        s.Draw();
      }
    }

    private static void DrawEnemiesShells()
    {
      foreach ( var s in EnemiesShells ) {
        s.Draw();
      }
    }

    /// <summary>
    /// タイトル画面の描画
    /// </summary>
    private static void DrawTitle()
    {
      DrawText( "Line of Battle", 50, 0, 200 );
      DrawText( "Press Left Mouse Button to Start", 25, 0, 300 );

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
    private static void DrawText( string text, double size, double x, double y )
    {
      var control = new ContentControl() {
        Width = Canvas.Width,
        Height = size,
        Content = new TextBlock() {
          Text = text,
          FontSize = size,
          Foreground = new SolidColorBrush( Color.FromRgb( 255, 255, 255 ) ),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
        },
      };

      Canvas.SetLeft( control, x );
      Canvas.SetTop( control, y );
      Buffer.Add( control );
    }

    public override void Render( RenderTarget target )
    {
      throw new NotImplementedException();
    }
  }
}
