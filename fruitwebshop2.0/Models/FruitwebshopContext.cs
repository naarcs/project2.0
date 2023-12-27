using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fruitwebshop2._0.Models;

public partial class FruitwebshopContext : DbContext
{
    public FruitwebshopContext()
    {
    }

    public FruitwebshopContext(DbContextOptions<FruitwebshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fruit> Fruits { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=fruitwebshop;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fruit>(entity =>
        {
            entity.HasKey(e => e.FruitId).HasName("PRIMARY");

            entity.ToTable("fruits");

            entity.Property(e => e.FruitId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.StockQuantity).HasColumnType("int(11)");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.UserId, "UserId").IsUnique();

            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp");
            entity.Property(e => e.TotalAmount).HasPrecision(10);
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PRIMARY");

            entity.ToTable("orderitems");

            entity.HasIndex(e => e.FruitId, "FruitId");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.Property(e => e.OrderItemId).HasColumnType("int(11)");
            entity.Property(e => e.FruitId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");

            entity.HasOne(d => d.Fruit).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.FruitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderitems_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderitems_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
