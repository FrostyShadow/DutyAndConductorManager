using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Conductors)
                .Map(m =>
                {
                    m.ToTable("ConductorRoles");
                    m.MapLeftKey("ConductorId");
                    m.MapRightKey("RoleId");
                });
            modelBuilder.Entity<Conductor>()
                .HasRequired(u => u.ConductorData)
                .WithRequiredPrincipal(r => r.Conductor)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.Messages)
                .WithRequired(r => r.SenderConductor)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.BrigadeConductors)
                .WithRequired(r => r.Conductor)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.MessageRecipients)
                .WithRequired(r => r.RecipientConductor)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.NotificationRecipients)
                .WithRequired(r => r.Conductor)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasMany(u => u.BrigadeConductors)
                .WithRequired(r => r.Brigade)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasRequired(u => u.Line)
                .WithMany(r => r.Brigades)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasRequired(u => u.VehicleSet)
                .WithMany(r => r.Brigades)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Line>()
                .HasRequired(u => u.LineType)
                .WithMany(r => r.Lines)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Message>()
                .HasMany(u => u.MessageRecipients)
                .WithRequired(r => r.Message)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Message>()
                .HasOptional(u => u.ParentMessage)
                .WithMany()
                .HasForeignKey(r => r.ParentMessageId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>()
                .HasMany(u => u.NotificationRecipients)
                .WithRequired(r => r.Notification)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Notification>()
                .HasRequired(u => u.CreatorConductor)
                .WithMany(r => r.Notifications)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Set>()
                .HasMany(u => u.SetVehicles)
                .WithRequired(r => r.Set)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Vehicle>()
                .HasRequired(u => u.VehicleType)
                .WithMany(r => r.Vehicles)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Vehicle>()
                .HasMany(u => u.SetVehicles)
                .WithRequired(r => r.Vehicle)
                .WillCascadeOnDelete(true);
        }
    }
}