using System;
using System.Windows;
using System.Windows.Input;

using System.Windows.Media.Media3D;
using System.Windows.Threading;

using Cyclone.WPF;
using Ballistic;
using Fireworks;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppBase app = new FireworksApp();
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);


        public MainWindow()
        {
            InitializeComponent();

            Model3DGroup holder = new Model3DGroup();
            ModelVisual3D visual = new ModelVisual3D { Content = holder };
            Scene.Children.Add(visual);

            app.Init(holder);

            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            app.Update(timer.Interval.TotalSeconds);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            app.OnMouseUp();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            app.OnKeyUp(e.Key);
        }
    }
}
