using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Set
    {
        public int SetId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SetVehicle> SetVehicles { get; set; }
        public virtual ICollection<Brigade> Brigades { get; set; }
    }

    public class SetVehicle
    {
        [Key, Column(Order = 0)]
        public int SetId { get; set; }
        public virtual Set Set { get; set; }
        [Key, Column(Order = 1)]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int Index { get; set; }
    }
}