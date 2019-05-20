using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Brigade
    {
        public int BrigadeId { get; set; }
        public int BrigadeNo { get; set; }
        public int LineId { get; set; }
        public virtual Line Line { get; set; }
        public int VehicleSetId { get; set; }
        public virtual Set VehicleSet { get; set; }
        public virtual ICollection<BrigadeConductor> BrigadeConductors { get; set; }
        public DateTime ServiceDateTime { get; set; }
    }

    public class BrigadeConductor
    {
        [Key, Column(Order = 0)]
        public int BrigadeId { get; set; }
        public virtual Brigade Brigade { get; set; }
        [Key, Column(Order = 1)]
        public int ConductorId { get; set; }
        public virtual Conductor Conductor { get; set; }
        public bool IsManager { get; set; }
    }
}