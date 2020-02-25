using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int currentPageNumber = 1;
        public int resultsByPage = 10;

        //players displayed in current page
        public List<Player> playersDisplayed = new List<Player>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VM();
        }

        private void SearchResults(object sender, RoutedEventArgs e)
        {
            //clear current list
            ResultsListBox.Items.Clear();
            VM v = (VM)this.DataContext;

            //Fetch results from ViewModel depending on filters
            playersDisplayed = v.players.Take(this.resultsByPage).ToList();

            //Add players to the results listbox
            foreach(Player p in playersDisplayed)
            {
                ResultsListBox.Items.Add(p.full_name);   
            }
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            //clear current list
            ResultsListBox.Items.Clear();
            VM v = (VM)this.DataContext;

            //Set current page
            currentPageNumber++;

            //Selection of the corresponding sublist
            playersDisplayed = v.players.Skip(currentPageNumber * resultsByPage)
                                        .Take(resultsByPage).ToList();

            //Add players to the results listbox
            foreach (Player p in playersDisplayed)
            {
                ResultsListBox.Items.Add(p.full_name);
            }
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            //clear current list
            ResultsListBox.Items.Clear();
            VM v = (VM)this.DataContext;

            //if we are at the base page we can't go previous
            if (currentPageNumber > 1)
            {
                //set current page
                currentPageNumber--;

                //Selection of the corresponding sublist
                playersDisplayed = v.players.Skip(currentPageNumber * resultsByPage)
                                            .Take(resultsByPage).ToList();

                //Add players to the results listbox
                foreach (Player p in playersDisplayed)
                {
                    ResultsListBox.Items.Add(p.full_name);
                }
            }
        }

        /*private void Rechercher(object sender, RoutedEventArgs e)
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
        }*/
    }
}


