using Ecommerce.Views;

namespace Ecommerce;

public partial class App : Application
{
    // Instancia de la base de datos
    private static DatabaseService _database;

    public static DatabaseService Database
    {
        get
        {
            if (_database == null)
            {
                _database = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3"));
            }
            return _database;
        }
    }

    public App(LoginPage loginPage)
	{
		InitializeComponent();

        var logueado = Preferences.Get("logueado", string.Empty);
        if (string.IsNullOrEmpty(logueado))
		{
			MainPage = loginPage;
        }
		else
		{
            MainPage = new AppShell();
        }

            
	}
}
