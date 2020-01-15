using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class VM : INotifyPropertyChanged
    {
        public String currentCapital;
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

        public VM()
        {
            model = new Model("../../country-capitals.csv");

            foreach (KeyValuePair<string, String> c in model.Countries)
            {
                countries.Add(new Country(c.Key, c.Value));
            }
            currentCapital = "oui";
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
}
