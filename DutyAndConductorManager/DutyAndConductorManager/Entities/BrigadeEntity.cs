using System;
using System.Collections.Generic;

namespace DutyAndConductorManager.Entities
{
    public class BrigadeEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int LineId { get; set; }
        public int VehicleSetId { get; set; }
        public DateTime ServiceDateTime { get; set; }

        public LineEntity Line { get; set; }
        public SetEntity Set { get; set; }
        public ICollection<BrigadeUserEntity> BrigadeUsers { get; set; }
    }

    public class BrigadeUserEntity
    {
        public int BrigadeId { get; set; }
        public int UserId { get; set; }
        public bool IsManager { get; set; }

        public BrigadeEntity Brigade { get; set; }
        public UserEntity User { get; set; }
    }
}