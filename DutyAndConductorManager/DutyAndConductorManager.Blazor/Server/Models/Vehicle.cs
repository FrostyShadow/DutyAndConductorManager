namespace DutyAndConductorManager.Blazor.Server.Models
{
    public class Vehicle
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SideNo { get; set; }
        public bool CanCouple { get; set; }
        public int TypeId { get; set; }
    }
}