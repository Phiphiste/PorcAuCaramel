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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VM();
        }

        private void Rechercher(object sender, RoutedEventArgs e)
        {
            //Player clicked on becomes the selected player
            Player selected = (Player) Players.SelectedItem;
            VM v = (VM) this.DataContext;
            v.Selected_player = selected;

            //erase the static text elements before appending them once again
            height.Text = String.Empty;
            weight.Text = String.Empty;
            work_timespan.Text = String.Empty;
            teams.Children.Clear();

            //Appending static text for a better data vizualisation
            height.Inlines.Add(selected.height + " cm");
            weight.Inlines.Add(selected.weight + " Kg");
            if(selected.work_period_end == " ")
            {
                work_timespan.Inlines.Add("Played from " + selected.work_period_start);
            }else work_timespan.Inlines.Add("Played from " + selected.work_period_start + " to " + selected.work_period_end);

            foreach (Team t in selected.teams)
            {
                TextBlock txt = new TextBlock();
                txt.Text = t.teamName;
                txt.HorizontalAlignment = HorizontalAlignment.Right;
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.TextWrapping = TextWrapping.Wrap;
                teams.Children.Add(txt);
            }
        }
    }
}


