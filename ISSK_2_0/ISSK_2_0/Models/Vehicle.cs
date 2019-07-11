using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class VehicleListView
    {
        public int VehicleId { get; set; }
        [Display(Name = "Numer taborowy")]
        public string SideNo { get; set; }
        [Display(Name = "Producent")]
        public string Manufacturer { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Czy można sprzęgać?")]
        public string IsCoupleableDescriptor => IsCoupleable ? "Tak" : "Nie";
        public bool IsCoupleable { get; set; }
        [Display(Name = "Typ pojazdu")]
        public string VehicleTypeName { get; set; }
    }

    public class VehicleCreateView
    {
        [Display(Name = "Numer taborowy")]
        public string SideNo { get; set; }
        [Display(Name = "Producent")]
        public string Manufacturer { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Czy można sprzęgać?")]
        public bool IsCoupleable { get; set; }
        [Display(Name = "Typ pojazdu")]
        public int VehicleTypeId { get; set; }

        public List<VehicleType> _vehicleTypes { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes => new SelectList(_vehicleTypes, "VehicleTypeId", "Name");
    }
}