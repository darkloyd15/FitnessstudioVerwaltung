using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Abschlussprojekt_FitnessstudioContext : DbContext
    {
        public Abschlussprojekt_FitnessstudioContext()
        {
        }

        public Abschlussprojekt_FitnessstudioContext(DbContextOptions<Abschlussprojekt_FitnessstudioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TrainingMachine> TrainingMachines { get; set; }
        public virtual DbSet<TrainingMachinePlan> TrainingMachinePlans { get; set; }
        public virtual DbSet<TrainingPlan> TrainingPlans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\;Initial Catalog=Abschlussprojekt_Fitnessstudio;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.StreetNumber).HasMaxLength(50);

                entity.Property(e => e.Streetname).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Emailaddress).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.SubscriptionNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Subscription)
                    .HasConstraintName("fk_CustomerSubscription");

                entity.HasOne(d => d.TrainingPlan)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.TrainingPlanId)
                    .HasConstraintName("fk_CustomerTrainingPlanId");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.AddressId, e.CustomerId })
                    .HasName("PK__Customer__9356CC500F51D7A4");

                entity.ToTable("CustomerAddress");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_addressId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customerId");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Emailaddress).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Address)
                    .HasConstraintName("fk_EmployeeAddressId");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.StartTime })
                    .HasName("PK__Schedule__BB5DCF9716E84A4A");

                entity.ToTable("Schedule");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ScheduleCustomerId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ScheduleEmployeeId");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TrainingMachine>(entity =>
            {
                entity.ToTable("TrainingMachine");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<TrainingMachinePlan>(entity =>
            {
                entity.HasKey(e => new { e.TraininMachineId, e.TrainingPlanId })
                    .HasName("PK__Training__526B3329E466B397");

                entity.ToTable("TrainingMachinePlan");

                entity.HasOne(d => d.TraininMachine)
                    .WithMany(p => p.TrainingMachinePlans)
                    .HasForeignKey(d => d.TraininMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_trainingMachine");

                entity.HasOne(d => d.TrainingPlan)
                    .WithMany(p => p.TrainingMachinePlans)
                    .HasForeignKey(d => d.TrainingPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_trainingPlan");
            });

            modelBuilder.Entity<TrainingPlan>(entity =>
            {
                entity.ToTable("TrainingPlan");

                entity.Property(e => e.Comment).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
