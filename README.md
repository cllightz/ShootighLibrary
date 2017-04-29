# 概要
WPF で DirectX による描画が可能なコントロールを実装した [dalance氏](http://qiita.com/dalance "Qiita") の D2dControl ([GitHub](https://github.com/dalance/D2dControl "GitHub"), [Qiita](http://qiita.com/dalance/items/f1af272279ac9b4f9dc9 "Qiita")) の、2D ゲーム向けのラッパークラスライブラリです。
シュータイライブラリと読みます。

## ライセンス
ひとまず MIT ライセンスとします。

## 動作環境
これ以外の環境でも動作するかもしれませんが、確認していません。
- Windows 7 以降の 64bit Windows
- .NET Framework 4.6.2 以上
- Visual Studio 2017

## 依存しているパッケージ
- D2dControl (v1.1.6)
- SharpDX (v3.1.1)
- SharpDX.Direct2D1 (v3.1.1)
- SharpDX.Direct3D11 (v3.1.1)
- SharpDX.Direct3D9 (v3.1.1)
- SharpDX.DXGI (v3.1.1)
- System.Numerics.Vectors (v4.3.0)
- System.ValueTuple (v4.3.0)

# 使用方法
## 準備
### ファイルをダウンロード
まずリポジトリのファイル群のトップ階層にあるいくつかの __ShootighLibrary.\*.\*.\*.nupkg__ の内、
__\*.\*.\*__ の部分の数字が最も大きいものをダウンロードしてください。

### NuGet パッケージソースの設定
NuGet のパッケージソースを設定し、
ダウンロードした .nupkg のファイルを NuGet から参照できるようにしてください。設定方法については、[Temarin氏](http://qiita.com/Temarin "Qiita")の記事が参考になると思います。

### NuGet 経由で ShootighLibrary をインストール
「参照」のタブでパッケージソースを「すべて」にして、"ShootighLibrary" と検索し、出てきたものをインストールしてください。

## 最低限のコーディング
### プロジェクトの作成
「新しいプロジェクト」で「Visual C#」の「WPF アプリ (.NET Framework)」を選択してください。

### MainWindow.xaml の修正箇所
`MainWindow.xaml` の `<Grid>` と `</Grid>` を削除してください。

### MainWindow.xaml.cs の修正箇所
`MainWindow.xaml.cs` のクラス `MainWindow` のコンストラクタ `MainWindow()` の `InitializeComponent()` 以降に、
以下のコードを追加してください。

```cs
var MainGrid = new Grid();
AddChild( MainGrid );
var MainControl = new ShootighLibrary.GameControl();
MainGrid.Children.Add( MainControl );
MainControl.SetGameInstance( new SampleGame( MainControl ) );
MainControl.SetEventHandlers( this );
```

### 抽象クラス Game の実装
`class SampleGame : ShootighLibrary.Game` のようにして、抽象クラス `Game` を継承・実装してください。
必要なメソッドは以下の通りです。
- コンストラクタ `public SampleGame( ShootighLibrary.GameControl control ) : base( control ) => Control = control`
- 抽象メソッドの実装 `public override void Initialize() { /* 初期化処理 */ }`
- 抽象メソッドの実装 `public override void MainLoop( SharpDX.Direct2D1.RenderTarget target ) { /* 毎フレームの処理 */ }`
