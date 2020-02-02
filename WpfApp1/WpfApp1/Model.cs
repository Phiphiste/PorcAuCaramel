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
                    Players.Add(new Player(full_name));
                }
            }
            data.Close();

            reader.Close();
        }

        public Model(String url)
        {
            GetPlayers(url);
        }
    }
}

