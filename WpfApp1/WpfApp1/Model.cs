using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    public class Model
    {
        public List<string> Countries = new List<string>();
        public List<Player> Players = new List<Player>();
        public List<Team> NBATeams = new List<Team>();
        public Dictionary<string, int> MVPs = new Dictionary<string, int>();
        public Dictionary<string, int> AllStars = new Dictionary<string, int>();
        public Dictionary<string, int> DPOYs = new Dictionary<string, int>();

        //Get the players from a wikidata query and store it in the List<Players> attribute
        //param : url of the query
        public async Task GetPlayers(string url)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; ");
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);

            //getting the json result back in a string 
            string result = reader.ReadToEnd();
            //converting it to a json object that we can go through
            dynamic jsonObj = JsonConvert.DeserializeObject(result);

            foreach (var obj in jsonObj)
            {
                foreach (var player in obj.Root.results.bindings)
                {
                    if(player.hauteur == null || player.masse == null || player.position_de_jeu_sp_cialit_Label == null || player.pays_de_nationalit_Label == null)
                    {
                        continue;
                    }
                    string full_name = (string)player.itemLabel.value;
                    //creation of our Player object from the full_name
                    Player p = new Player(full_name);

                    p.height = (float)player.hauteur.value;
                    p.weight = (float)player.masse.value;
                    p.position = (string)player.position_de_jeu_sp_cialit_Label.value;
                    p.nationality = (string)player.pays_de_nationalit_Label.value;

                    //Deletion of the time from the birthdate (which was always 00:00:00)
                    string birthdate = (string)player.date_de_naissance.value;
                    string[] strlist = birthdate.Split(' ');
                    p.birthdate = strlist[0];

                    //this field is often null so a check is needed
                    //if it's empty, we assign a blank value
                    if (player.d_but_de_la_p_riode_d_activit_ != null)
                    {
                        string work_period_start = (string)player.d_but_de_la_p_riode_d_activit_.value;
                        strlist = work_period_start.Split(' ');
                        work_period_start = strlist[0].Substring(strlist[0].Length - 4);
                        p.work_period_start = work_period_start;
                    }
                    else p.work_period_start = " ";

                    //this field is often null so a check is needed
                    //if it's empty we assign a blank value too
                    if (player.fin_de_la_p_riode_d_activit_ != null)
                    {
                        string work_period_end = (string)player.fin_de_la_p_riode_d_activit_.value;
                        strlist = work_period_end.Split(' ');
                        work_period_end = strlist[0].Substring(strlist[0].Length - 4);
                        p.work_period_end = work_period_end;
                    }
                    else p.work_period_end = " ";

                    if (player.image != null)
                    {
                        p.imageURL = player.image.value;
                    }
                    else p.imageURL = null;
                    
                    //check if the player was a part of an actual nba team, if not he is discarded
                    if (player.membre_de_l__quipe_de_sportLabel != null)
                    {
                        Team t = findTeamInNBATeams((string)player.membre_de_l__quipe_de_sportLabel.value);
                        if(t!=null)
                        {
                            p.teams.Add(t);
                            Players.Add(p);
                        }
                    }
                }
            }
            data.Close();

            reader.Close();
        }
        
        //Finds the Team object in the static collection of Teams
        //param : the team's full name (ex : San Antonio Spurs)
        public Team findTeamInNBATeams(string team_name)
        {
            foreach(Team t in NBATeams)
            {
                if (t.teamName == team_name)
                    return t;
            }
            return null;
        }

        //Read and import all the NBA Teams from a static JSON file saved within the solution
        public void GetTeams()
        {
            string json = System.IO.File.ReadAllText("../../teams.json");
            NBATeams = JsonConvert.DeserializeObject<List<Team>>(json);
        }

        public void GetCountries()
        {
            using (var reader = new StreamReader("../../country-capitals.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    String[] values = line.Split(',');
                    Countries.Add(values[0]);
                }

            }
        }

        public void GetMVPs()
        {
            using (var reader = new StreamReader("../../MVPs.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    String[] values = line.Split(';');
                    MVPs.Add(values[0],Int32.Parse(values[1]));
                }

            }
        }

        public void GetAllStars()
        {
            using (var reader = new StreamReader("../../AllStars.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    String[] values = line.Split(';');
                    
                    if(!AllStars.ContainsKey(values[0]) && values[0] != "")
                        AllStars.Add(values[0], Int32.Parse(values[1]));
                }

            }
        }

        public void GetDefensivePlayer()
        {
            using (var reader = new StreamReader("../../defensivePlayer.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    String[] values = line.Split(';');

                    if (!DPOYs.ContainsKey(values[0]) && values[0] != "")
                        DPOYs.Add(values[0], Int32.Parse(values[1]));
                }

            }
        }

        //Constructor of the model, initiates the teams and fetchs the players corresponding to the given query
        //param: query url
        public Model(String url)
        {
            GetTeams();
            GetPlayers(url);
            GetCountries();
            GetMVPs();
            GetAllStars();
            GetDefensivePlayer();
        }
    }

    public class Player
    {

        public string full_name { get; set; }
        public string birthdate { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public string position { get; set; }
        public string nationality { get; set; }
        public string work_period_start { get; set; }
        public string work_period_end { get; set; }
        public string imageURL { get; set; }
        public List<Team> teams { get; set; }

        public Dictionary<string,int> awards { get; set; }

        public Player(string full_name)
        {
            teams = new List<Team>();
            awards = new Dictionary<string, int>();
            this.full_name = full_name;
        }
    }

    public class Team
    {
        public string abbreviation { get; set; }
        public string teamName { get; set; }
        public string simpleName { get; set; }
        public string location { get; set; }

        public Team(string name)
        {
            this.teamName = name;
        }
    }
}

