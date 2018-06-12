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
using System.IO;

namespace IntroApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0); //Exist when the video ends
        }
        private void video_Loaded(object sender, RoutedEventArgs e)
        {
            string str = System.IO.Path.GetTempPath() + "v.mp4";
            File.WriteAllBytes(str, Properties.Resources.REPLACEME); //Change to the name of the resource file example Properties.Resources.Video
            this.video.Source = new Uri(str);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += new SizeChangedEventHandler(this.Window_SizeChanged); //do not change
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.video.StretchDirection = StretchDirection.UpOnly; //do no change
        }
        private void main_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; //makes it so ALT+F4
        }
        private void video_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; //make it so ALT + F4 dosent work
        }
    }
}
