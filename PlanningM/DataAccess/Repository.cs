using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PlanningM.Measurement.Model;

namespace PlanningM.DataAccess
{
    public class Repository : IRepository
    {
        public ObservableCollection<CityM> LoadCollectionCityM()
        {            
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<CityM>));
            using (StreamReader rd = new StreamReader(Path.GetFullPath(@"..\..\") + "save.xml"))
            {
                ObservableCollection<CityM> Cityes = xs.Deserialize(rd) as ObservableCollection<CityM>;
                return Cityes;
            }
        }

        public void SaveCollectionCityM(ObservableCollection<CityM> Cityes)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<CityM>));
            using (StreamWriter wr = new StreamWriter(Path.GetFullPath(@"..\..\") + "save.xml"))
            {
                xs.Serialize(wr, Cityes);
            }
        }
    }
}
