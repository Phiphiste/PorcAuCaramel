using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace WpfApp1
{
    class Model
    {
        public List<Player> Players = new List<Player>();
        public List<Team> NBATeams = new List<Team>();

        public void GetPlayers(string url)
        {
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
                    string full_name = (string)player.itemLabel.value;
                    Player p = new Player(full_name);

                    p.height = (int)player.hauteur.value;
                    p.weight = (int)player.masse.value;
                    p.position = (string)player.position_de_jeu_sp_cialit_Label.value;
                    p.nationality = (string)player.pays_de_nationalit_Label.value;
                    p.birthdate = (string)player.date_de_naissance.value;

                    if (player.d_but_de_la_p_riode_d_activit_ != null)
                    {
                        string work_period_start = (string)player.d_but_de_la_p_riode_d_activit_.value;
                        work_period_start.Substring(work_period_start.Length - 4);
                        p.work_period_start = work_period_start;
                    }
                    else p.work_period_start = " ";

                    if (player.fin_de_la_p_riode_d_activit_ != null)
                    {
                        string work_period_end = (string)player.fin_de_la_p_riode_d_activit_.value;
                        work_period_end.Substring(work_period_end.Length - 4);
                        p.work_period_end = work_period_end;
                    }
                    else p.work_period_end = " ";
                    

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

        public Team findTeamInNBATeams(string team_name)
        {
            foreach(Team t in NBATeams)
            {
                if (t.teamName == team_name)
                    return t;
            }
            return null;
        }

        public void GetTeams()
        {
            string json = System.IO.File.ReadAllText("../../teams.json");
            NBATeams = JsonConvert.DeserializeObject<List<Team>>(json);
        }

        public Model(String url)
        {
            GetTeams();
            GetPlayers(url);
        }
    }
}

