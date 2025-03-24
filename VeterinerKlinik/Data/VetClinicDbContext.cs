using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinik.Models;

namespace VeterinerKlinik.Data;

public partial class VetClinicDbContext : DbContext
{
    public VetClinicDbContext()
    {
    }

    public VetClinicDbContext(DbContextOptions<VetClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnimalGroup> AnimalGroups { get; set; }

    public virtual DbSet<Inspection> Inspections { get; set; }

    public virtual DbSet<Inventory> Inventory { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=YEC_Laptop\\SQLEXPRESS;Database=VetClinicDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__ANIMAL_G__D57795A07E6F2304");

            entity.ToTable("ANIMAL_GROUPS");

            entity.HasIndex(e => e.GroupName, "UQ_group_name").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .HasColumnName("group_name");
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.HasKey(e => e.InspectionId).HasName("PK__INSPECTI__C3C4E74369671E8E");

            entity.ToTable("INSPECTIONS");

            entity.Property(e => e.InspectionId).HasColumnName("inspection_id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.Diagnosis).HasColumnName("diagnosis");
            entity.Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasColumnName("exam_date");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Symptoms).HasColumnName("symptoms");
            entity.Property(e => e.Treatment).HasColumnName("treatment");

            entity.HasOne(d => d.Pet).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examinations_Pets");

            entity.HasOne(d => d.Staff).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examinations_Staff");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__INVENTOR__52020FDDCAFA179F");

            entity.ToTable("INVENTORY");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__OWNERS__3C4FBEE4F8DBBA91");

            entity.ToTable("OWNERS");

            entity.HasIndex(e => e.IdentityNumber, "UQ_identity_number").IsUnique();

            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasDefaultValue("Adres Beliritilmedi")
                .HasColumnName("address");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("identity_number");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.PetId).HasName("PK__PETS__390CC5FE45C3DF5D");

            entity.ToTable("PETS");

            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Age)
                .HasComputedColumnSql("(datediff(year,[birth_date],getdate()))", false)
                .HasColumnName("age");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .HasColumnName("breed");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.MicrochipNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ÇİP TAKILMAMIŞ")
                .HasColumnName("microchip_number");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.Species)
                .HasMaxLength(50)
                .HasColumnName("species");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Group).WithMany(p => p.Pets)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pets_AnimalGroups");

            entity.HasOne(d => d.Owner).WithMany(p => p.Pets)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pets_Owners");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__PRESCRIP__3EE444F89EB8E6A7");

            entity.ToTable("PRESCRIPTIONS");

            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.InspectionId).HasColumnName("inspection_id");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UsageInstructions).HasColumnName("usage_instructions");

            entity.HasOne(d => d.Inspection).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.InspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Inspections");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Inventory");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__RECEIPTS__91F52C1F0C0F17A3");

            entity.ToTable("RECEIPTS");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasDefaultValue("Nakit")
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Ödendi")
                .HasColumnName("payment_status");
            entity.Property(e => e.ReceiptDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("receipt_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Owner).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Owners");

            entity.HasOne(d => d.Staff).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Staff");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__RECEIPT___38E9A22416061E35");

            entity.ToTable("RECEIPT_DETAILS");

            entity.Property(e => e.DetailId).HasColumnName("detail_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.InspectionId).HasColumnName("inspection_id");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

            entity.HasOne(d => d.Inspection).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.InspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptDetails_Inspections");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptDetails_Receipts");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__STAFF__1963DD9CC38C3C9A");

            entity.ToTable("STAFF");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.WorkStartDate).HasColumnName("work_start_date");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("PK__VACCINAT__E588AFE743903FDC");

            entity.ToTable("VACCINATIONS");

            entity.Property(e => e.VaccinationId).HasColumnName("vaccination_id");
            entity.Property(e => e.NextVaccinationDate).HasColumnName("next_vaccination_date");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.VaccinationDate).HasColumnName("vaccination_date");
            entity.Property(e => e.VaccineId).HasColumnName("vaccine_id");

            entity.HasOne(d => d.Pet).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccinations_Pets");

            entity.HasOne(d => d.Staff).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccinations_Staff");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccinations_Inventory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
