using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ecommerce.ViewsModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;
        private string _errorMessage;
        private bool _isErrorVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        // Mensaje de error para la validación
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        // Controla la visibilidad del mensaje de error
        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                _isErrorVisible = value;
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }

        [RelayCommand]
        private async Task Login()
        {
            var user = await App.Database.GetUserAsync(Usuario);

            if (user != null && user.Password == Password)
            {
                // Limpiar mensaje de error
                IsErrorVisible = false;
                ErrorMessage = string.Empty;

                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Por favor, complete todos los campos.";
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Por favor, complete todos los campos", "Aceptar");
                    IsErrorVisible = true; // Mostrar el mensaje de error
                }
                else
                {

                    Preferences.Set("logueado", "si");
                    Application.Current.MainPage = new AppShell();
                    await Application.Current.MainPage.DisplayAlert("Inicio de sesión", "¡Inicio de sesión exitoso!", "Aceptar");                    
                }
                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fallido", "Nombre de usuario o contraseña no válidos. Por favor, inténtelo de nuevo.", "Aceptar");
            }
        }

        // Método para actualizar las propiedades
        protected virtual async void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        private async Task Register()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());// Volver a la página de inicio de sesión

           Application.Current.MainPage=new RegisterPage();
        }
    }
}
