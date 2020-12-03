using DutyAndConductorManager.Blazor.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutyAndConductorManager.Blazor.Server.Models
{
    public class DutyAndConductorContext : DbContext
    {
        public DutyAndConductorContext(DbContextOptions<DutyAndConductorContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrigadeEntity>()
                .HasMany(b => b.BrigadeUsers)
                .WithOne(bu => bu.Brigade)
                .HasForeignKey(bu => bu.BrigadeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BrigadeEntity>()
                .HasOne(b => b.Set)
                .WithMany(s => s.Brigades)
                .HasForeignKey(b => b.VehicleSetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BrigadeEntity>()
                .HasOne(b => b.Line)
                .WithMany(l => l.Brigades)
                .HasForeignKey(b => b.LineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BrigadeUserEntity>().HasKey(bu => new {bu.BrigadeId, bu.UserId});

            modelBuilder.Entity<LineEntity>()
                .HasOne(l => l.LineType)
                .WithMany(lt => lt.Lines)
                .HasForeignKey(l => l.LineTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationEntity>()
                .HasMany(n => n.NotificationRecipients)
                .WithOne(nr => nr.Notification)
                .HasForeignKey(nr => nr.NotificationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationEntity>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationRecipientEntity>().HasKey(nr => new {nr.NotificationId, nr.UserId});

            modelBuilder.Entity<RoleEntity>()
                .ToTable("UserRoles")
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles);

            modelBuilder.Entity<SetEntity>()
                .HasMany(s => s.SetVehicles)
                .WithOne(sv => sv.Set)
                .HasForeignKey(sv => sv.SetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SetVehicleEntity>().HasKey(sv => new {sv.SetId, sv.VehicleId});

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.UserData)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDataEntity>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.BrigadeUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.NotificationRecipients)
                .WithOne(nr => nr.User)
                .HasForeignKey(nr => nr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(v => v.SetVehicles)
                .WithOne(sv => sv.Vehicle)
                .HasForeignKey(sv => sv.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VehicleEntity>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserDataEntity> UserData { get; set; }
        public DbSet<BrigadeEntity> Brigades { get; set; }
        public DbSet<BrigadeUserEntity> BrigadeUsers { get; set; }
        public DbSet<LineEntity> Lines { get; set; }
        public DbSet<LineTypeEntity> LineTypes { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<NotificationRecipientEntity> NotificationRecipients { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<VehicleTypeEntity> VehicleTypes { get; set; }
    }
}