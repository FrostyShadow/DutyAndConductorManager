using System;
using System.Collections.Generic;

namespace DutyAndConductorManager.Entities
{
    public class LineEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int LineTypeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public LineTypeEntity LineType { get; set; }
        public ICollection<BrigadeEntity> Brigades { get; set; }
    }

    public class LineTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LineEntity> Lines { get; set; }
    }
}