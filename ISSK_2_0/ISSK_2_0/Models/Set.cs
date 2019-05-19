using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SetVehicle
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public virtual Set Set { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int Index { get; set; }
    }
}