using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace PlanningM.Measurement.Model
{
    public class CityM
    {        
        public string Name { get; set; }  
        
        public ObservableCollection<ClientM> Clientes { get; set; }
        public ObservableCollection<DayM> Days { get; set; }

        public CityM()
        {                        
        }

        public CityM(string name, ObservableCollection<ClientM> clientes, ObservableCollection<DayM> days)
        {
            Name = name;
            Clientes = clientes;
            Days = days;
        }        
    }
}
