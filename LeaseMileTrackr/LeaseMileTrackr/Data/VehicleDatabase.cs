using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using LeaseMileTrackr.Models;

namespace LeaseMileTrackr.Data
{
    public class VehicleDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public VehicleDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Vehicle>().Wait();
        }

        public Task<List<Vehicle>> GetVehiclesAsync()
        {
            return _database.Table<Vehicle>().ToListAsync();
        }

        public Task<Vehicle> GetVehicleAsync(int id)
        {
            return _database.Table<Vehicle>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveVehicleAsync(Vehicle vehicle)
        {
            vehicle.Title = vehicle.Year + " " + vehicle.Make + " " + vehicle.Model;
            if (vehicle.ID != 0)
            {
                return _database.UpdateAsync(vehicle);
            }
            else
            {
                return _database.InsertAsync(vehicle);
            }
        }

        public Task<int> DeleteVehicleAsync(Vehicle vehicle)
        {
            return _database.DeleteAsync(vehicle);
        }
    }
}
