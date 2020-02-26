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
        public ObservableCollection<string> countries = new ObservableCollection<string>();
        public ObservableCollection<Team> teams = new ObservableCollection<Team>();
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
            var query_size = 20000;
            var url = "https://query.wikidata.org/sparql?format=json&action=wbgetentities&query=SELECT%20%3FitemLabel%20%3Fhauteur%20%3Fmasse%20%3Fposition_de_jeu_sp_cialit_Label%20%3Fpays_de_nationalit_Label%20%3Fdate_de_naissance%20%3Fmembre_de_l__quipe_de_sportLabel%20%3Fd_but_de_la_p_riode_d_activit_%20%3Ffin_de_la_p_riode_d_activit_%20WHERE%20%7B%0A%20%20SERVICE%20wikibase%3Alabel%20%7B%20bd%3AserviceParam%20wikibase%3Alanguage%20%22%5BAUTO_LANGUAGE%5D%2Cen%22.%20%7D%0A%20%20%3Fitem%20wdt%3AP31%20wd%3AQ5%3B%0A%20%20%20%20wdt%3AP106%20wd%3AQ3665646%3B%0A%20%20%20%20wdt%3AP2685%20%3Fid.%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2048%20%3Fhauteur.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP54%20%3Fmembre_de_l__quipe_de_sport.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2067%20%3Fmasse.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP413%20%3Fposition_de_jeu_sp_cialit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP27%20%3Fpays_de_nationalit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP569%20%3Fdate_de_naissance.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2561%20%3Fnom.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2031%20%3Fd_but_de_la_p_riode_d_activit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2032%20%3Ffin_de_la_p_riode_d_activit_.%20%7D%0A%7D%0ALIMIT%20"+query_size;
            var url_nolimit = "https://query.wikidata.org/sparql?format=json&action=wbgetentities&query=SELECT%20%3FitemLabel%20%3Fhauteur%20%3Fmasse%20%3Fposition_de_jeu_sp_cialit_Label%20%3Fpays_de_nationalit_Label%20%3Fdate_de_naissance%20%3Fmembre_de_l__quipe_de_sportLabel%20%3Fd_but_de_la_p_riode_d_activit_%20%3Ffin_de_la_p_riode_d_activit_%20WHERE%20%7B%0A%20%20SERVICE%20wikibase%3Alabel%20%7B%20bd%3AserviceParam%20wikibase%3Alanguage%20%22%5BAUTO_LANGUAGE%5D%2Cen%22.%20%7D%0A%20%20%3Fitem%20wdt%3AP31%20wd%3AQ5%3B%0A%20%20%20%20wdt%3AP106%20wd%3AQ3665646%3B%0A%20%20%20%20wdt%3AP2685%20%3Fid.%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2048%20%3Fhauteur.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP54%20%3Fmembre_de_l__quipe_de_sport.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2067%20%3Fmasse.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP413%20%3Fposition_de_jeu_sp_cialit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP27%20%3Fpays_de_nationalit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP569%20%3Fdate_de_naissance.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2561%20%3Fnom.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2031%20%3Fd_but_de_la_p_riode_d_activit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2032%20%3Ffin_de_la_p_riode_d_activit_.%20%7D%0A%7D%0A";
            model = new Model(url_nolimit);
            //go through the result of the query
            foreach(Player p in model.Players)
            {
                //check if this version of the player is already added to the list
                Player existing_player = getPlayerFromPlayers(p.full_name);
                //if the result is null, it's the first encounter with this Player and its directly added
                if (existing_player == null)
                    players.Add(p);
                //if it's not null, we already have this player and it must be a version with a different team
                else
                {
                    //if this version of the player has a team that is not in its teams alread
                    if (getTeamFromTeams(existing_player.teams,p.teams.First().teamName) == null)
                        //we add it to the Player object we already have
                        existing_player.teams.Add(p.teams.First());
                }
            }

            foreach(string country in model.Countries)
            {
                countries.Add(country);
            }

            foreach(Team team in model.NBATeams)
            {
                teams.Add(team);
            }
        }

        public List<Player> getResearchResult(Dictionary<string,string> filters)
        {
            //initially the results are all the players
            List<Player> results = players.ToList();

            //loop through the DB to remove those who don't fit the filters
            foreach(Player p in players)
            {
                //Full Name
                if (filters.ContainsKey("FullName"))
                {
                    //if the name doesn't contain what we're looking for, remove the player and go to the net one
                    if (!p.full_name.ToLower().Contains(filters["FullName"].ToLower()))
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Nationality
                if (filters.ContainsKey("Nationality"))
                {
                    if (!p.nationality.ToLower().Contains(filters["Nationality"].ToLower()))
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Year of birth
                if (filters.ContainsKey("BirthYear"))
                {
                    if (!p.birthdate.Contains(filters["BirthYear"]))
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Team -- Not sure
                if (filters.ContainsKey("Team"))
                {
                    foreach(Team t in p.teams)
                    {
                        if(!t.teamName.Contains(filters["Team"]))
                        {
                            results.Remove(p);
                            continue;
                        }
                    }                  
                }

                //Position -- Not sure
                if (filters.ContainsKey("Position"))
                {
                    if (!p.position.ToLower().Contains(filters["Position"].ToLower()))
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Active Player
                if (filters.ContainsKey("isActive"))
                {
                    if(p.work_period_end != " ")
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Height
                if (filters.ContainsKey("Height"))
                {
                    if (!p.height.ToString().Contains(filters["Height"]))
                    {
                        results.Remove(p);
                        continue;
                    }
                }

                //Weight
                if (filters.ContainsKey("Weight"))
                {
                    if (!p.weight.ToString().Contains(filters["Weight"]))
                    {
                        results.Remove(p);
                        continue;
                    }
                }
            }
            return results;
        }

        //Returns a Team object from a Team List
        //params: the list to get the object from, the name of the team we want
        public Team getTeamFromTeams(List<Team> teams, string team_name)
        {
            foreach(Team t in teams)
            {
                if (t.teamName == team_name)
                    return t;
            }
            return null;
        }

        //Returns a Player object from the current list of players
        //params: the full name of the desired player
        public Player getPlayerFromPlayers(string full_name)
        {
            foreach(Player p in players)
            {
                if (p.full_name == full_name)
                    return p;
            }
            return null;
        }

        //Binding with the interface for the selected player
        public Player Selected_player
        {
            get { return this.selected_player; }

            set
            {
                this.selected_player = value;
                OnPropertyChanged("Selected_player");
            }
        }

        //Binding with the interface for the players
        public ObservableCollection<Player> Players
        {
            get { return this.players; }
            
            set
            {
                this.players = value;
                OnPropertyChanged("Players");
            }
        }

        //Binding with the interface for the countries
        public ObservableCollection<string> Countries
        {
            get { return this.countries; }

            set
            {
                this.countries = value;
                OnPropertyChanged("Countries");
            }
        }

        //Binding with the interface for the teams
        public ObservableCollection<Team> Teams
        {
            get { return this.teams; }

            set
            {
                this.teams = value;
                OnPropertyChanged("Teams");
            }
        }
    }


}
