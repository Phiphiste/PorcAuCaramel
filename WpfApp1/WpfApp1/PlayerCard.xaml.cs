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
                ListBoxItem item = new ListBoxItem();
                item.Content = a.Key + "x " + a.Value;
                Awards_.Items.Add(item);
            }

            if (p.imageURL != null)
                image_player.Source = new BitmapImage(new Uri(p.imageURL));
            else image_player.Source = new BitmapImage(new Uri("https://moonvillageassociation.org/wp-content/uploads/2018/06/default-profile-picture1.jpg"));
        }
    }
}
