using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ViewsModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;

        [RelayCommand]
        private async Task Login()
        {
            var user = await App.Database.GetUserAsync(Usuario);

            if (user != null && user.Password == Password)
            {
                Preferences.Set("logueado", "si");
                Application.Current.MainPage = new AppShell();
                await Application.Current.MainPage.DisplayAlert("Inicio de sesión", "¡Inicio de sesión exitoso!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fallido", "Nombre de usuario o contraseña no válidos. Por favor, inténtelo de nuevo.", "Aceptar");
            }
        }

        [RelayCommand]
        private async Task Register()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());// Volver a la página de inicio de sesión

           Application.Current.MainPage=new RegisterPage();
        }
    }
}
