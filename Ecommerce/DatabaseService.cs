using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Modelos;
using SQLite;

namespace Ecommerce
{
   public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait(); // Crear tabla de usuarios si no existe
        }

        // Obtener todos los usuarios
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        // Obtener un usuario por nombre de usuario
        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>().Where(u => u.Email == username).FirstOrDefaultAsync();
        }

        // Agregar un nuevo usuario
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }
    }
}
