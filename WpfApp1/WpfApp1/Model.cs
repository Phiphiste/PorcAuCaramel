using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Model
    {
        public Dictionary<String, String> Countries = new Dictionary<string, string>();

        public void ReadCsv(String filename)
        {

            using (var reader = new StreamReader(filename))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    String[] values = line.Split(',');

                    Countries.Add(values[0], values[1]);
                }

            }
        }

        public Model(String filename)
        {
            ReadCsv(filename);
        }

        public String getCap(String Country)
        {
            return Countries[Country];
        }
    }
}

