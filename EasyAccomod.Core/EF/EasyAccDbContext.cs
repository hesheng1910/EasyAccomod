
using EasyAccomod.Core.Configurations;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAccomod.Core.EF
{
    public class EasyAccDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public EasyAccDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserLikePostConfiguration());
            modelBuilder.ApplyConfiguration(new InfrastructureConfiguration());
            modelBuilder.ApplyConfiguration(new AddressNearByConfiguration());
            modelBuilder.ApplyConfiguration(new RequestExtendConfiguration());
            modelBuilder.ApplyConfiguration(new DateViewPostConfiguration());
            modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<long>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<long>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            //Data Seeding
            modelBuilder.Seed();
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<UserLikePost> UserLikePosts { get; set; }
        public DbSet<Infrastructure> Infrastructures { get; set; }
        public DbSet<AddressNearBy> AddressNearBies { get; set; }
        public DbSet<RequestExtend> RequestExtends { get; set; }
        public DbSet<DateViewPost> DateViewPosts { get; set; }
    }
}