using System;
using System.Windows;

namespace ShootighLibrary
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App() {
                StartupUri = new Uri( "MainWindow.xaml", UriKind.Relative )
            };

            app.InitializeComponent();
            app.Run();
        }
    }
}
