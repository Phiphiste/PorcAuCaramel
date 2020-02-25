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
        public int currentPageNumber = 0;
        public int resultsByPage = 10;
        public int numberOfPages;

        //players displayed in current page
        public List<Player> playersDisplayed = new List<Player>();

        //All players corresponding to research
        public List<Player> fullQueryResult = new List<Player>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VM();
        }

        public void DisplayCurrentPage()
        {
            //clear current list
            ResultsListBox.Items.Clear();

            //Fetch results from ViewModel depending on filters
            playersDisplayed = fullQueryResult.Skip(currentPageNumber * resultsByPage)
                                              .Take(resultsByPage).ToList();

            //Add players to the results listbox
            foreach (Player p in playersDisplayed)
            {
                ResultsListBox.Items.Add(p.full_name);
            }

            //if it's the first page, disable "Previous" button
            if (currentPageNumber == 0)
                PreviousBtn.IsEnabled = false;
            else PreviousBtn.IsEnabled = true;

            //if it's the last page, disable "Next" button
            if ((currentPageNumber+1) == numberOfPages)
                NextBtn.IsEnabled = false;
            else NextBtn.IsEnabled = true;

            //Display current page number
            CurrentPageTextBlock.Text = "Page " + (currentPageNumber + 1).ToString() + " of " + numberOfPages.ToString();
        }

        private void SearchResults(object sender, RoutedEventArgs e)
        {
            VM v = (VM)this.DataContext;
            //allPlayers = v.getResearchResult();
            fullQueryResult = v.players.ToList();
            numberOfPages = (fullQueryResult.Count() / resultsByPage) + 1;

            ResultsPanel.Visibility = Visibility.Visible;

            DisplayCurrentPage();
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            //Set current page
            currentPageNumber++;

            DisplayCurrentPage();
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            //if we are at the base page we can't go previous
            if (currentPageNumber > 0)
            {
                //set current page
                currentPageNumber--;

                DisplayCurrentPage();
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


