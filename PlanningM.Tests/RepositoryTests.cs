using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanningM.DataAccess;
using PlanningM.Measurement.Model;
using PlanningM.Measurement.ViewModel;

namespace PlanningM.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void LoadCollectionCityM_returnCityM()
        {
            // arrange
            IRepository rep = new Repository();

            // act
            var result = rep.LoadCollectionCityM();
            var actual = result[0];

            // assert
            Assert.IsInstanceOfType(actual, typeof(CityM));
        }
    }

    [TestClass]
    public class MeasurementMTests
    {
        [TestMethod]
        public void BuildCalendar_CityM_returnDays()
        {
            // arrange            
            MeasurementM measurementVM = new MeasurementM();
            CityM cityes = new CityM();

            XmlSerializer xs = new XmlSerializer(typeof(Collection<CityM>));
            using (StreamReader rd = new StreamReader(Path.GetFullPath(@"..\..\") + "save.xml"))
            {
                Collection<CityM> Cityes = xs.Deserialize(rd) as Collection<CityM>;
                cityes = Cityes[0];
            }

            // act
            var result = measurementVM.BuildCalendar(cityes);
            var actual = result[0];

            // assert
            Assert.IsInstanceOfType(actual, typeof(DayM));
        }
    }
}
