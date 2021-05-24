﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ron.Ido.EM.Entities;

namespace Ron.Ido.EM
{
    public class AppDbContext: DbContext
    {
        public virtual DbSet<Apply> Applies { get; set; }
        public virtual DbSet<ApplyAim> ApplyAims { get; set; }
        public virtual DbSet<ApplyBarCode> ApplyBarCodes { get; set; }
        public virtual DbSet<ApplyDeliveryForm> ApplyDeliveryForms { get; set; }
        public virtual DbSet<ApplyDocFullPackageType> ApplyDocFullPackageTypes { get; set; }
        public virtual DbSet<ApplyDocType> ApplyDocTypes { get; set; }
        public virtual DbSet<ApplyEntryForm> ApplyEntryForms { get; set; }
        public virtual DbSet<ApplyLearnForm> ApplyLearnForms { get; set; }
        public virtual DbSet<ApplyPassportType> ApplyPassportTypes { get; set; }

        public virtual DbSet<ApplyTemplate> ApplyTemplates { get; set; }


        public virtual DbSet<Country> Countries { get; set; } 

        public virtual DbSet<FileInfo> FileInfos { get; set; }
        public virtual DbSet<LearnLevel> LearnLevels { get; set; }
        public virtual DbSet<Legalization> Legalizations { get; set; }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolesPermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UsersRoles { get; set; }

        private IDbContextTransaction _transaction = null;

        public AppDbContext(DbContextOptions options) : base(options) { }

        public bool BeginTransaction()
        {
            if (_transaction != null)
                return false;

            _transaction = Database.BeginTransaction();
            return true;
        }

        public void Commit()
        {
            try
            {
                if (_transaction == null)
                    return;

                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
                BeginTransaction();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
                return;

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
            BeginTransaction();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<ApplyDocType>()
                .HasOne(doctype => doctype.LearnLevel)
                .WithMany(level => level.ApplyDocTypes)
                .HasForeignKey(doctype => doctype.LearnLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(p => p.Role)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
