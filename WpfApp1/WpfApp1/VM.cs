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
        public String currentCapital;
        public List<Player> test;
        public Model model;
        public ObservableCollection<Country> countries = new ObservableCollection<Country>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        

        private static List<Player> GetPlayers(string url)
        {
            List<Player> players = new List<Player>();

            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; ");

            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);

            string result = reader.ReadToEnd();

            dynamic jsonObj = JsonConvert.DeserializeObject(result);
            
            foreach (var obj in jsonObj)
            {
                foreach (var player in obj.Root.results.bindings)
                {
                    players.Add(new Player((string)player.itemLabel.value));
                }
            }
            data.Close();

            reader.Close();

            return players;
        }

        public VM()
        {
            test = GetPlayers("https://query.wikidata.org/sparql?format=json&action=wbgetentities&query=SELECT%20%3Fitem%20%3FitemLabel%20%3Fid%20WHERE%20%7B%0A%20%20SERVICE%20wikibase%3Alabel%20%7B%20bd%3AserviceParam%20wikibase%3Alanguage%20%22%5BAUTO_LANGUAGE%5D%2Cen%22.%20%7D%0A%20%20%3Fitem%20wdt%3AP31%20wd%3AQ5%3B%0A%20%20%20%20wdt%3AP106%20wd%3AQ3665646.%0A%20%20%3Fitem%20wdt%3AP2685%20%3Fid.%20%0A%7D%0ALimit%2020");

            model = new Model("../../country-capitals.csv");

            foreach (KeyValuePair<string, String> c in model.Countries)
            {
                countries.Add(new Country(c.Key, c.Value));
            }
            currentCapital = "";
        }

        public String CurrentCapital
        {
            get { return this.currentCapital; }

            set
            {
                this.currentCapital = value;
                OnPropertyChanged("CurrentCapital");
            }
        }


        public ObservableCollection<Country> Countries
            {
            get { return this.countries; }
            
            set
            {
                this.countries = value;
                OnPropertyChanged("Countries");
            }
        }
    }

    public class Country 
    {
        public String name { get; set; }
        public String capital { get; set; }
        public Country (String n, String c)
        {
            name = n;
            capital = c;
        }     
    }

    public class Player
    {
        public string full_name { get; set; }
        /*public string last_name;
        public DateTime birthdate;
        public int height;
        public int weight;
        public Team team;*/

        public Player(string name)
        {
            this.full_name = name;
        }
    }

    public class PlayersDB
    {
        public List<Player> results;
    }
    
    public class Team
    {
        public string name;
        public string city;
        public DateTime creation;
    }
}
