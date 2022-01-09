using System;
using SQLite;

namespace LeaseMileTrackr.Models
{
    public class Vehicle
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime LeaseStart { get; set; }
        public int LengthMonths { get; set; }
        public int StartMiles { get; set; }
        public int AllotedMiles { get; set; }
        public string Title { get; set; }
        public int CalculatedMiles { get; set; }

    }
}
