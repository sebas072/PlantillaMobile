using appPrueba.Models;
using appPrueba.Services;
using appPrueba.Views;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appPrueba.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        public Command LoginIn { get; set; }
        public Command HuellaIn { get; set; }
        public Login User { get; set; }
        public IDataStore<Login> DataStoreLogin => DependencyService.Get<IDataStore<Login>>() ?? new UserDtaStore();
        public LoginViewModel()
        {
            User = new Login();
            Title = "Iniciar sesión";
            LoginIn = new Command(async () => await ExecuteLoginInCommand());
            HuellaIn = new Command(async () => await ExecuteHuellaInCommand());
            
        }
        private async Task ExecuteHuellaInCommand()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("Ingresa tu huella");
            if (result.Authenticated)
            {
                await IniciarSeccion(true);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Huella no valida", "OK");
            }
        }

       
        async Task ExecuteLoginInCommand()
        {
            if (await ValidateCampos())
            {
                await IniciarSeccion(false);
            }
        }

        async Task IniciarSeccion(bool sw)
        {
            ShowLoading(true);
            bool IsSucces;
            if (!sw)
            {

                if (netService.IsConected())
                {
                    var response = await apiService.APICosumeGetiD<Login>(generateUrl());
                    IsSucces = response.IsSucces;
                }
                else
                {
                    var user = await DataStoreLogin.GetItemAsync(User.usuario1, User.pass);
                    IsSucces = user != null;
                }
            }
            else {
               IsSucces = sw;
            }
            await repoceLoginIn(IsSucces);        
            ShowLoading(false);
        }

        private async Task<bool> ValidateCampos()
        {
            if (string.IsNullOrEmpty(User.usuario1) || string.IsNullOrEmpty(User.pass))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Campos vacios", "OK");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
      
        async Task repoceLoginIn(bool isSucces)
        {
            if (isSucces)
            {
                if (User.usuario1!= null)
                {
                    await DataStoreLogin.DeleteItemAsync(User.usuario1);
                    await DataStoreLogin.AddItemAsync(User);
                }
                App.Current.MainPage = new MainPage();
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos o revisa tu conexion", "OK");
            }
        }
        private string generateUrl()
        {
            return $"/api/Usuarios/{User.usuario1}";
        }
    }
}
