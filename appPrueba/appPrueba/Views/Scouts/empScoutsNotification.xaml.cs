using appPrueba.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appPrueba.Views.Scouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class empScoutsNotification : ContentPage
    {
        NotificationViewModel viewModel;
        public empScoutsNotification()
        {
            InitializeComponent();  
            BindingContext = this.viewModel = new NotificationViewModel();
        }

        private  void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            viewModel.guardarSw();
        }
    }
}