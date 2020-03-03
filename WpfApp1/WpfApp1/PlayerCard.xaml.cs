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
    /// Logique d'interaction pour PlayerCard.xaml
    /// </summary>
    public partial class PlayerCard : Window
    {
        public Player player;

        public PlayerCard(Player p)
        {
            InitializeComponent();
            this.Title = p.full_name;
            player = p;

            Name.Text = p.full_name;
            Nationality.Text = p.nationality;
            Height.Text = p.height.ToString() + "cm";
            Weight.Text = p.weight.ToString() + "kg";
            Position.Text = p.position;
            Work_timespan.Text = p.work_period_start + " - " + p.work_period_end;

            foreach (Team t in p.teams)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = t.teamName +", "+ t.abbreviation;
                Teams_.Items.Add(item);
            }

            foreach (KeyValuePair<string,int> a in p.awards)
            {
                //creation of the listbox item
                ListBoxItem item = new ListBoxItem();
                item.Content = a.Value + "x " + a.Key;
                item.Width = 100;
                item.VerticalAlignment = VerticalAlignment.Center; 

                //creation of the image 
                Image award_logo = new Image();
                if (a.Key == "MVP")
                    award_logo.Source = new BitmapImage(new Uri("https://i.cdn.turner.com/nba/nba/heat/media/kia_mvp_logo.png"));
                else if (a.Key == "AllStar") award_logo.Source = new BitmapImage(new Uri("https://content.sportslogos.net/logos/6/980/full/4251__nba_all-star_game-alternate-2017.png"));
                else award_logo.Source = new BitmapImage(new Uri("https://hooptactics.net/premium/defense/common/images/nobasketlogo.png"));
                award_logo.Height = 40;
                award_logo.Width = 40;

                //creation of a panel to contain both the text and the image together
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Children.Add(award_logo);
                panel.Children.Add(item);
                
                Awards_.Items.Add(panel);
            }

            if (p.imageURL != null)
                image_player.Source = new BitmapImage(new Uri(p.imageURL));
            else image_player.Source = new BitmapImage(new Uri("https://moonvillageassociation.org/wp-content/uploads/2018/06/default-profile-picture1.jpg"));
        }
    }
}
