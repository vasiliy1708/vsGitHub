using System;
using System.Windows;
using System.Windows.Controls;

namespace PlanningM.Measurement.View
{
    /// <summary>
    /// Логика взаимодействия для ViewChangeDate.xaml
    /// </summary>
    public partial class ViewChangeDate : Window
    {
        public ViewChangeDate()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2.5;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2.5;
           
            InitializeComponent();            
        }        

        private void Clndr_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;            

            if (calendar.SelectedDate.HasValue)
            {
                DateTime cSelectedDateValue = calendar.SelectedDate.Value;
                
                if (DateTime.Compare(cSelectedDateValue, DateTime.Today) >= 0)
                {                    
                    calendar.SelectedDate = null;
                    this.Close();                    
                }
                else
                {                    
                    calendar.SelectedDate = null;                    
                }
            }            
        }
    }
}
