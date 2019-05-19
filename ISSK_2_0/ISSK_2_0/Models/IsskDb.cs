using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ISSK_2_0.Models
{
    public class IsskDb : DbContext
    {
        public IsskDb() : base("IsskDb") { }

        public DbSet<Conductor> Conductors { get; set; }
        public DbSet<ConductorData> ConductorData { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<BrigadeConductor> BrigadeConductors { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineType> LineTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<SetVehicle> SetVehicles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Conductors)
                .Map(m =>
                {
                    m.ToTable("ConductorRoles");
                    m.MapLeftKey("Id");
                    m.MapRightKey("Id");
                });
            modelBuilder.Entity<Conductor>()
                .HasRequired(u => u.ConductorData)
                .WithRequiredPrincipal(r => r.Conductor)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.Messages)
                .WithRequired(r => r.SenderConductor)
                .WillCascadeOnDelete(true);

        }
    }
}