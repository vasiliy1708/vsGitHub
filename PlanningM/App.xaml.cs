using System;
using System.Windows;
using PlanningM.Measurement;
using PlanningM.Measurement.View;
using PlanningM.Measurement.ViewModel;

namespace PlanningM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {             
                
                MeasurementM measurementVM = new MeasurementM();               
                ViewChangeDate viewChangeDate = null;
                
                measurementVM.ShowDateScreenAction += () =>
                {
                    try
                    {                        
                        viewChangeDate = new ViewChangeDate();                        
                        viewChangeDate.DataContext = measurementVM;                      
                        viewChangeDate.ShowDialog();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "App ShowDateScreenAction");                        
                        Application.Current.Shutdown();
                    }
                };

                ViewMeasurement view = new ViewMeasurement();
                view.DataContext = measurementVM;
                view.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "App Application_Startup");                
                Application.Current.Shutdown();
            }
        }
    }
}
