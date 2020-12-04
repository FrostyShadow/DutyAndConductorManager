using System.Collections.Generic;

namespace DutyAndConductorManager.Entities
{
    public class VehicleEntity
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SideNo { get; set; }
        public bool CanCouple { get; set; }
        public int VehicleTypeId { get; set; }

        public VehicleTypeEntity VehicleType { get; set; }
        public ICollection<SetVehicleEntity> SetVehicles { get; set; }
    }

    public class VehicleTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}