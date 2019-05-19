using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Line
    {
        public int Id { get; set; }
        public string LineNumber { get; set; }
        public int TypeId { get; set; }
        public virtual LineType LineType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class LineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
    }
}