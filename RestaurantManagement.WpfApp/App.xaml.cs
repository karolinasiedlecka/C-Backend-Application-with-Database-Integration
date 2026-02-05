using System.Windows;
using RestaurantManagement.Views;

namespace RestaurantManagement.WpfApp
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                Window1 selectionWindow = new Window1();
                selectionWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Błąd startu: " + ex.Message);
            }
        }
    }
}