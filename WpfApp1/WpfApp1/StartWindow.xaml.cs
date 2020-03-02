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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("../../background.jpg", UriKind.Relative));
            this.Background = myBrush;
        }

        public void Search(object sender, RoutedEventArgs e)
        {
            int qs = 0;
            bool success = Int32.TryParse(QuerySizeTextBox.Text, out qs);
            if (success || QuerySizeTextBox.Text == String.Empty)
            {
                MainWindow mw = new MainWindow(qs);
                mw.Show();
                this.Hide();
            }
            else
            {
                QSLabel.Content = "Error : not a number. Please try again";
                QSLabel.Foreground = Brushes.Red;
            } 
        }
    }
}
