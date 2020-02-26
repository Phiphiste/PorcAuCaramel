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

        //Dictionary of filters
        public Dictionary<string, string> filters = new Dictionary<string, string>();

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
            BuildFiltersDict();

            VM v = (VM)this.DataContext;

            fullQueryResult = v.getResearchResult(filters);

            //compute the number of pages based on the result by page
            numberOfPages = (fullQueryResult.Count() / resultsByPage) + 1;

            //reset current page
            currentPageNumber = 0;            

            //Make the results appear
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
            //Set current page
            currentPageNumber--;

            DisplayCurrentPage();
        }

        public void BuildFiltersDict()
        {
            filters.Clear();

            //Full Name
            if(FullNameTextBox.Text != String.Empty)
                filters.Add("FullName", FullNameTextBox.Text);

            //Nationality
            if(NationalityComboBox.SelectedItem != null)
                filters.Add("Nationality", NationalityComboBox.SelectedItem.ToString());

            //Year of birth
            if (BirthdateTextBox.Text != String.Empty)
                filters.Add("BirthYear", BirthdateTextBox.Text);

            //Team
            if (TeamComboBox.SelectedItem != null)
                filters.Add("Team", TeamComboBox.Text);

            //Position
            if (PositionComboBox.SelectedItem != null)
                filters.Add("Position", PositionComboBox.Text);

            //Active Player
            if (ActivePlayerCheckBox.IsChecked == true)
                filters.Add("isActive", "True");

            //Height
            if (HeightTextBox.Text != String.Empty)
                filters.Add("Height", HeightTextBox.Text);

            //Weight
            if (WeightTextBox.Text != String.Empty)
                filters.Add("Weight", WeightTextBox.Text);
        }

        public void ClearFilters(object sender, RoutedEventArgs e)
        {
            //clear the textBoxes
            FullNameTextBox.Clear();
            BirthdateTextBox.Clear();
            HeightTextBox.Clear();
            WeightTextBox.Clear();

            //select empty item for comboBoxes
            NationalityComboBox.SelectedIndex = -1;
            TeamComboBox.SelectedIndex = -1;
            PositionComboBox.SelectedIndex = -1;

            //untick checkBoxes
            ActivePlayerCheckBox.IsChecked = false;
        }

        public void SeeCard(object sender, RoutedEventArgs e)
        {
            //get the Player object from the ViewModel
            VM v = (VM)this.DataContext;
            Player p = v.getPlayerFromPlayers(ResultsListBox.SelectedItem.ToString());

            //Instantiate new PlayerCard window / pass the corresponding player
            PlayerCard pc = new PlayerCard(p);
            pc.Show();
        }
    }
}


