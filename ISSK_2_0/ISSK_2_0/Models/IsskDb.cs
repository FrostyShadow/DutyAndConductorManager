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
                .HasOptional(u => u.ConductorData)
                .WithRequired(r => r.Conductor)
                .WillCascadeOnDelete(true);    
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.BrigadeConductors)
                .WithRequired(r => r.Conductor)
                .HasForeignKey(u => u.ConductorId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.MessageRecipients)
                .WithRequired(r => r.RecipientConductor)
                .HasForeignKey(u => u.RecipientId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Conductor>()
                .HasMany(u => u.NotificationRecipients)
                .WithRequired(r => r.Conductor)
                .HasForeignKey(u => u.ConductorId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasMany(u => u.BrigadeConductors)
                .WithRequired(r => r.Brigade)
                .HasForeignKey(u => u.BrigadeId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasRequired(u => u.Line)
                .WithMany(r => r.Brigades)
                .HasForeignKey(u => u.LineId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Brigade>()
                .HasRequired(u => u.VehicleSet)
                .WithMany(r => r.Brigades)
                .HasForeignKey(u => u.VehicleSetId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Line>()
                .HasRequired(u => u.LineType)
                .WithMany(r => r.Lines)
                .HasForeignKey(u => u.LineTypeId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Message>()
                .HasRequired(u => u.SenderConductor)
                .WithMany(r => r.Messages)
                .HasForeignKey(u => u.SenderId);
            modelBuilder.Entity<Message>()
                .HasMany(u => u.MessageRecipients)
                .WithRequired(r => r.Message)
                .HasForeignKey(u => u.MessageId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Message>()
                .HasOptional(u => u.ParentMessage)
                .WithMany()
                .HasForeignKey(r => r.ParentMessageId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>()
                .HasMany(u => u.NotificationRecipients)
                .WithRequired(r => r.Notification)
                .HasForeignKey(u => u.NotificationId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Notification>()
                .HasRequired(u => u.CreatorConductor)
                .WithMany(r => r.Notifications)
                .HasForeignKey(u => u.CreatorId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Set>()
                .HasMany(u => u.SetVehicles)
                .WithRequired(r => r.Set)
                .HasForeignKey(u => u.VehicleId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Vehicle>()
                .HasRequired(u => u.VehicleType)
                .WithMany(r => r.Vehicles)
                .HasForeignKey(u => u.TypeId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Vehicle>()
                .HasMany(u => u.SetVehicles)
                .WithRequired(r => r.Vehicle)
                .HasForeignKey(u => u.SetId)
                .WillCascadeOnDelete(true);
        }
    }
}