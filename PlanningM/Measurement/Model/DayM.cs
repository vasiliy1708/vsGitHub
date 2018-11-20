using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlanningM.Measurement.Model
{
    public class DayM : INotifyPropertyChanged
    {
        private DateTime? _date;
        private int _maxCount;
        private bool _isTargetMonth;
        private bool _enabled;

        public int MaxCount
        {
            get => _maxCount;
            set { _maxCount = value; OnPropertyChanged(); }
        }

        public bool IsTargetMonth
        {
            get { return _isTargetMonth; }
            set { _isTargetMonth = value; OnPropertyChanged(); }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; OnPropertyChanged(); }
        }

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
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