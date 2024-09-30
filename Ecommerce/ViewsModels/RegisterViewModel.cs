using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ViewsModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;

        [RelayCommand]
        private async Task Register()
        {
            var existingUser = await App.Database.GetUserAsync(Usuario);

            if (existingUser == null)
            {
                // Crear un nuevo usuario y guardarlo en la base de datos
                var newUser = new User
                {
                    Email = Usuario,
                    Password = Password
                };

                await App.Database.SaveUserAsync(newUser);

                await Application.Current.MainPage.DisplayAlert("Éxito", "El usuario se registró correctamente. Ya puedes iniciar sesión.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();// Volver a la página de inicio de sesión
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El nombre de usuario ya existe. Por favor, elija uno diferente.", "Aceptar");
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}
