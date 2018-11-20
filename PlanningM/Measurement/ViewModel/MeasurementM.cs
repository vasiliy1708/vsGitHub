using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PlanningM.Measurement.Common;
using PlanningM.Measurement.Model;
using PlanningM.DataAccess;

namespace PlanningM.Measurement.ViewModel
{
    public class MeasurementM : INotifyPropertyChanged
    {
        private IRepository _repository;
        private CityM _selectedCityes;
        private DateTime? _selectedDate;

        public ObservableCollection<DayM> Days { get; set; }
        //public ObservableCollection<string> DayNames { get; set; }        
        public ObservableCollection<CityM> Cityes { get; set; }

        static string GetDescription(Enum enumElement)
        {
            var field = enumElement.GetType().GetField(enumElement.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : enumElement.ToString();
        }

        public MeasurementM()
        {                        
            //DayNames = new ObservableCollection<string> { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            Days = new ObservableCollection<DayM>();
                        
            _repository = new Repository();
            LoadCityMFromRepository();  
            //SaveCityMToRepo();
            SetupCommands();            
        }

        private void LoadCityMFromRepository()
        {
            Cityes = _repository.LoadCollectionCityM();
            
            SelectFirstCityes();                        
            OnPropertyChanged();
        }

       

        #region Commands
        private DelegateCommand _showChangeDate;
        private ClientM _selectedClient;

        public event Action ShowDateScreenAction;

        public ICommand ShowChangeDate
        {
            get { return _showChangeDate; }
        }
        #endregion Commands

        private void SetupCommands()
        {           
            _showChangeDate = new DelegateCommand(ShowDateScreen);
        }

        // Show calendar window
        private void ShowDateScreen()
        {
            if (ShowDateScreenAction != null)            
                ShowDateScreenAction.Invoke();
            
            _selectedClient = null;
        }

        // Select client from list
        public ClientM SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;                
                if(value!= null) ShowDateScreen();
                OnPropertyChanged();
            }
        }

        // City selection
        public CityM SelectedCityes
        {
            get => _selectedCityes;
            set
            {
                _selectedCityes = value;
                BuildCalendar(value);
                OnPropertyChanged();
            }
        }

        // First city to the dropdown list
        private void SelectFirstCityes()
        {
            if (Cityes != null)
                if (Cityes.Count > 0)
                    SelectedCityes = Cityes[0];
        }

        // Date selection
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value;
                if (value != null) ChangeMaxCount(value);
                OnPropertyChanged(); }
        }

        // Logic of change of a limit
        private void ChangeMaxCount(DateTime? date)
        {
            string dateToString = ((DateTime) date).ToLongDateString();

            if (DateTime.Compare((DateTime)date, DateTime.Today) >= 0)
            {
                foreach (DayM dayM in Days)
                {
                    if (DateTime.Equals(date, dayM.Date))
                    {                        
                        if (dayM.MaxCount > 0)
                        {                            
                            //Дата назначается клиенту впервые или изменяется?
                            if (!_selectedClient.DateMeasurement.HasValue)
                            {
                                int a = dayM.MaxCount - 1;
                                dayM.MaxCount = a;
                                _selectedClient.DateMeasurement = date;
                            }
                            else
                            {                                
                                if (!DateTime.Equals(date, _selectedClient.DateMeasurement))
                                {                                    
                                    dayM.MaxCount = dayM.MaxCount - 1;
                                    foreach (DayM dayMPlus in Days)
                                    {
                                        if (DateTime.Equals(_selectedClient.DateMeasurement, dayMPlus.Date))
                                        {                                            
                                            dayMPlus.MaxCount = dayMPlus.MaxCount + 1;
                                        }
                                    }

                                    _selectedClient.DateMeasurement = date;
                                }
                            }
                        }
                        else MessageBox.Show("Лимит " + dateToString + " исчерпан. \nВыберите дату с доступным лимитом.", "Внимание!"); break;
                    }                                            
                }

                
                if (DateTime.Compare((DateTime)Days[Days.Count-1].Date, (DateTime)date) < 0)
                {
                    MessageBox.Show("Выберите дату из ближайших семи дней","Внимание!");
                }
            }
            else MessageBox.Show("Нельзя запланировать прошедшую дату", "Выбрано " + dateToString + "(сегодня " + DateTime.Today.Date.ToLongDateString() + ")");
        }

        #region Days
        // We form days. Seven days from today.
        public ObservableCollection<DayM> BuildCalendar(CityM selectedCityes)
        {
            Days.Clear();
            DateTime targetDate = DateTime.Today;

            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);                       
            d = d.AddDays(targetDate.Day - 1);

            for (int box = 1; box <= 7; box++)
            {
                DayM day = new DayM { Date = d, Enabled = true, IsTargetMonth = targetDate.Month == d.Month };
                foreach (DayM dayM in selectedCityes.Days)
                {
                    if (Equals(day.Date, dayM.Date))
                    {
                        day.MaxCount = dayM.MaxCount;
                    }
                }
                Days.Add(day);
                d = d.AddDays(1);
            }
            return Days;
        }

        public void BuildCalendar2()
        {
            Days.Clear();
            DateTime targetDate = DateTime.Today;
            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);
            d = d.AddDays(targetDate.Day - 1);
            for (int box = 1; box <= 7; box++)
            {
                DayM day = new DayM { Date = d, Enabled = true, IsTargetMonth = targetDate.Month == d.Month };                
                Days.Add(day);
                d = d.AddDays(1);
            }
        }
        

        private static int DayOfWeekNumber(DayOfWeek dow)
        {
            return Convert.ToInt32(dow.ToString("D"));
        }
        #endregion
        
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
