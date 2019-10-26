using appPrueba.Models;
using appPrueba.ViewModels;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appPrueba.Views.HuellaDigital
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteDatailPage : ContentPage
    {
        public ClienteDatailPage(ClientDetailViewModel clientDetailViewModel)
        {
            InitializeComponent();
            if (clientDetailViewModel != null)
            {
                BindingContext = clientDetailViewModel;
            }
            else {
                BindingContext = new ClientDetailViewModel(null);
            }
           
        }
    }
}