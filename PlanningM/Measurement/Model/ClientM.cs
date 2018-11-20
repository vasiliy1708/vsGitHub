using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlanningM.Measurement.Model
{
    public class ClientM : INotifyPropertyChanged
    {
        private DateTime? _dateMeasurement;       

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientCity { get; set; }

        public DateTime? DateMeasurement
        {
            get { return _dateMeasurement; }
            set { _dateMeasurement = value; OnPropertyChanged(); }
        }

        public ClientM()
        {
        }

        public ClientM(int clientId ,string clientName,string clientPhone,string clientCity, DateTime? dateMeasurement)
        {
            ClientId = clientId;
            ClientName = clientName;
            ClientPhone = clientPhone;
            ClientCity = clientCity;
            DateMeasurement = dateMeasurement;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
