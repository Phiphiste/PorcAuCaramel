using MySqlX.Serialization;
using Newtonsoft.Json;
using NUglify.JavaScript;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WpfApp1
{
    class VM : INotifyPropertyChanged
    {
        public ObservableCollection<Player> players = new ObservableCollection<Player>();
        public Player selected_player { get; set; }
        public Model model;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public VM()
        {
            model = new Model("https://query.wikidata.org/sparql?format=json&action=wbgetentities&query=SELECT%20%3Fitem%20%3FitemLabel%20%3Fid%20WHERE%20%7B%0A%20%20SERVICE%20wikibase%3Alabel%20%7B%20bd%3AserviceParam%20wikibase%3Alanguage%20%22%5BAUTO_LANGUAGE%5D%2Cen%22.%20%7D%0A%20%20%3Fitem%20wdt%3AP31%20wd%3AQ5%3B%0A%20%20%20%20wdt%3AP106%20wd%3AQ3665646.%0A%20%20%3Fitem%20wdt%3AP2685%20%3Fid.%20%0A%7D%0ALimit%2020");
            foreach(Player p in model.Players)
            {
                players.Add(p);
            }

            selected_player = new Player("j j");
        }

        public Player Selected_player
        {
            get { return this.selected_player; }

            set
            {
                this.selected_player = value;
                OnPropertyChanged("Selected_player");
            }
        }

        public ObservableCollection<Player> Players
        {
            get { return this.players; }
            
            set
            {
                this.players = value;
                OnPropertyChanged("Players");
            }
        }
    }

    public class Player
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birthdate;
        public int height;
        public int weight;
        public Team team;

        public Player(string full_name)
        {
            String[] separator = { " " };
            String[] strlist = full_name.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            this.first_name = strlist[0];
            this.last_name = strlist[1];
        }
    }
    
    public class Team
    {
        public string name;
        public string city;
        public DateTime creation;
    }
}
