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
        }
    }
}
