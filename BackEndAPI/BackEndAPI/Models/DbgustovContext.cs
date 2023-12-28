using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Models;

public partial class DbgustovContext : DbContext
{
    public DbgustovContext()
    {
    }

    public DbgustovContext(DbContextOptions<DbgustovContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Vacacion> Vacacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__6C985625E53701CB");

            entity.ToTable("Cargo");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D26010763");

            entity.ToTable("Departamento");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9ECB462347");

            entity.ToTable("Empleado");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaContrato).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Sueldo).HasColumnName("sueldo");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK__Empleado__IdCarg__2B3F6F97");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Empleado__IdDepa__2A4B4B5E");
        });

        modelBuilder.Entity<Vacacion>(entity =>
        {
            entity.HasKey(e => e.IdVacaciones).HasName("PK__Vacacion__DD71531216471B5F");

            entity.ToTable("Vacacion");

            entity.Property(e => e.Detalle)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Gestion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Vacacions)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Vacacione__IdEmp__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
