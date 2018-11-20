using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanningM.Measurement.Model;

namespace PlanningM.DataAccess
{
    /// <summary>
    /// Load and save db
    /// </summary>
    public interface IRepository
    {        
        ObservableCollection<CityM> LoadCollectionCityM();
        void SaveCollectionCityM(ObservableCollection<CityM> Cityes);
    }
}
