using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyAndConductorManager.Blazor.Server.Entities
{
    public class SetEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SetVehicleEntity> SetVehicles { get; set; }
        public ICollection<BrigadeEntity> Brigades { get; set; }
    }

    public class SetVehicleEntity
    {
        public int SetId { get; set; }
        public int VehicleId { get; set; }
        public int Index { get; set; }

        public SetEntity Set { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}