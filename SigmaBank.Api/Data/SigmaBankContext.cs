#nullable disable

using Microsoft.EntityFrameworkCore;
using SigmaBank.Api.Data.Models;

namespace SigmaBank.Api.Data;

public class SigmaBankContext : DbContext
{
    public SigmaBankContext()
    {
    }

    public SigmaBankContext(DbContextOptions<SigmaBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountCredit> AccountCredits { get; set; }

    public virtual DbSet<AccountDebit> AccountDebits { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Mcc> Mccs { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Name=ConnectionStrings:SigmaBank");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pk");

            entity.ToTable("account");

            entity.HasIndex(e => e.CardNumber, "account_card_number_uq").IsUnique();

            entity.HasIndex(e => e.UserId, "account_user_id_uq").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasPrecision(30, 10)
                .HasColumnName("balance");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(19)
                .HasColumnName("card_number");
            entity.Property(e => e.CashbackAmount)
                .HasPrecision(30, 10)
                .HasColumnName("cashback_amount");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_currency_id_fk");

            entity.HasOne(d => d.User).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_user_id_fk");
        });

        modelBuilder.Entity<AccountCredit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_credit_pk");

            entity.ToTable("account_credit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(30, 10)
                .HasColumnName("amount");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountCreditAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_credit_account_id_fk");

            entity.HasOne(d => d.Sender).WithMany(p => p.AccountCreditSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("account_credit_account_id_fk2");
        });

        modelBuilder.Entity<AccountDebit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_debit_pk");

            entity.ToTable("account_debit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(30, 10)
                .HasColumnName("amount");
            entity.Property(e => e.Cashback)
                .HasPrecision(30, 10)
                .HasColumnName("cashback");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountDebitAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_debit_account_id_fk");

            entity.HasOne(d => d.Recipient).WithMany(p => p.AccountDebitRecipients)
                .HasForeignKey(d => d.RecipientId)
                .HasConstraintName("account_debit_account_id_fk2");

            entity.HasOne(d => d.Shop).WithMany(p => p.AccountDebits)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("account_debit_shop_id_fk");
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("credit_pk");

            entity.ToTable("credit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(30, 10)
                .HasColumnName("amount");
            entity.Property(e => e.ApprovalTimestamp).HasColumnName("approval_timestamp");
            entity.Property(e => e.CreationTimestamp).HasColumnName("creation_timestamp");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Months).HasColumnName("months");
            entity.Property(e => e.Percent).HasColumnName("percent");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Credits)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("credit_account_id_fk");

            entity.HasOne(d => d.Manager).WithMany(p => p.Credits)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("credit_employee_id_fk");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("currency_pk");

            entity.ToTable("currency");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsoCode)
                .HasMaxLength(3)
                .HasColumnName("iso_code");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pk");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Login, "employee_login_uq").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FistName)
                .HasMaxLength(32)
                .HasColumnName("fist_name");
            entity.Property(e => e.Login)
                .HasMaxLength(32)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(32)
                .HasColumnName("patronymic");
            entity.Property(e => e.SecondName)
                .HasMaxLength(32)
                .HasColumnName("second_name");
            entity.Property(e => e.SignupTimestamp).HasColumnName("signup_timestamp");

            entity.HasMany(d => d.Roles).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("employee_role_role_id_fk"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("employee_role_employee_id_fk"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "RoleId").HasName("employee_role_pk");
                        j.ToTable("employee_role");
                        j.IndexerProperty<Guid>("EmployeeId").HasColumnName("employee_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Mcc>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("mcc_pk");

            entity.ToTable("mcc");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("region_pk");

            entity.ToTable("region");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shop_pk");

            entity.ToTable("shop");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddressUri)
                .HasMaxLength(2048)
                .HasColumnName("address_uri");
            entity.Property(e => e.CashbackPercent)
                .HasPrecision(30, 10)
                .HasColumnName("cashback_percent");
            entity.Property(e => e.MccCode).HasColumnName("mcc_code");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");

            entity.HasOne(d => d.MccCodeNavigation).WithMany(p => p.Shops)
                .HasForeignKey(d => d.MccCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shop_mcc_code_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_uq").IsUnique();

            entity.HasIndex(e => e.PassportNumber, "user_passport_number_uq").IsUnique();

            entity.HasIndex(e => e.Phone, "user_phone_uq").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeExpiration).HasColumnName("code_expiration");
            entity.Property(e => e.ConfirmationCode)
                .HasMaxLength(4)
                .HasColumnName("confirmation_code");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.FailedAttempts).HasColumnName("failed_attempts");
            entity.Property(e => e.FistName)
                .HasMaxLength(32)
                .HasColumnName("fist_name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(10)
                .HasColumnName("passport_number");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(32)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.PhotoName)
                .HasMaxLength(256)
                .HasColumnName("photo_name");
            entity.Property(e => e.RegionCode).HasColumnName("region_code");
            entity.Property(e => e.SecondName)
                .HasMaxLength(32)
                .HasColumnName("second_name");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.SignupTimestamp).HasColumnName("signup_timestamp");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.RegionCodeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RegionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_region_code_fk");
        });
    }
}