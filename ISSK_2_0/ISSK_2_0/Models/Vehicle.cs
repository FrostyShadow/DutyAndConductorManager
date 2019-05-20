using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SideNo { get; set; }
        public bool IsCoupleable { get; set; }
        public int TypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<SetVehicle> SetVehicles { get; set; }
    }

    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}