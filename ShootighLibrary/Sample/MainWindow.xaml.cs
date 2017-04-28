using System.Windows;
using System.Windows.Controls;

namespace Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid SampleGrid;
        SampleControl MainControl;

        public MainWindow()
        {
            InitializeComponent();
            SampleGrid = new Grid();
            AddChild( SampleGrid );
            MainControl = new SampleControl();
            SampleGrid.Children.Add( MainControl );
            MainControl.SetGameInstance( new SampleGame( MainControl ) );
            MainControl.SetEventHandlers( this );
        }
    }
}
