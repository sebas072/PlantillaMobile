using Xamarin.Forms;
using appPrueba.Services;
using appPrueba.Views;
using appPrueba.Models;

namespace appPrueba
{
    public partial class App : Application
    {
        public bool Remember { get; set; }
        public App()
        {
            InitializeComponent();
            isRemember();
            if (Remember)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
           
        }
       
        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected async override void OnSleep()
        {
            
        }
        protected override void OnResume()
        {
            
        }
        private async void isRemember()
        {
            IDataStore<Login> da = DependencyService.Get<IDataStore<Login>>() ?? new UserDtaStore();
            var user = await da.GetItemsAsync();
            Login us = new Login();
            foreach (var u in user)
                us = u;

            Remember = us.Remember;
        }

    }
}
