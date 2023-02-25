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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Media3D;
using System.Windows.Threading;

using Ballistic;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BallisticApp ballistic = new BallisticApp();
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            Model3DGroup holder = new Model3DGroup();
            ModelVisual3D visual = new ModelVisual3D { Content = holder };
            Scene.Children.Add(visual);

            ballistic.Init(holder);

            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ballistic.Update(timer.Interval.TotalSeconds);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ballistic.OnMouseUp();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ballistic.OnKeyUp(e.Key);
        }
    }
}
