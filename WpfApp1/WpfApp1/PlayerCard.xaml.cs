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
            if (p.imageURL != null)
                Photo.Source = new BitmapImage(new Uri(p.imageURL));
            else Photo.Source = new BitmapImage(new Uri("https://moonvillageassociation.org/wp-content/uploads/2018/06/default-profile-picture1.jpg"));
        }
    }
}
