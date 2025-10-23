using Xamarin.Forms;

namespace HostelApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Services.DataStore.Current.InitSampleData();

            // Оборачиваем табы в NavigationPage (только здесь!)
            MainPage = new NavigationPage(new Pages.MainTabbedPage());
        }
    }
}
