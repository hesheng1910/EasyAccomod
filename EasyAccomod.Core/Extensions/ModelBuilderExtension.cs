using EasyAccomod.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Extensions
{
    public static class ModelBuilderExtension 
    {
        public static void Seed (this ModelBuilder model)
        {
            model.Entity<AppRole>().HasData(
                                new AppRole()
                                {
                                    Id = 1,
                                    Name = "ADMIN",
                                    NormalizedName = "ADMIN",
                                    Description = "Adminstrator Role"
                                },
                                new AppRole()
                                {
                                    Id = 2,
                                    Name = "MODERATOR",
                                    NormalizedName = "MODERATOR",
                                    Description = "Employee Role"
                                },
                                new AppRole()
                                {
                                    Id = 3,
                                    Name = "OWNER",
                                    NormalizedName = "OWNER",
                                    Description = "Owner Role"
                                },
                                new AppRole()
                                {
                                    Id = 4,
                                    Name = "RENTER",
                                    NormalizedName = "RENTER",
                                    Description = "Renter Role"
                                });
            var hasher = new PasswordHasher<AppUser>();
            model.Entity<AppUser>().HasData(
                                    new AppUser()
                                    {
                                        Id = 1,
                                        UserName = "admin",
                                        NormalizedUserName = "admin",
                                        SecurityStamp = string.Empty,
                                        PasswordHash = hasher.HashPassword(null, "Abc@1234"),
                                        Address = "Tran Thai Tong",
                                        FirstName = "Hoa",
                                        LastName = "Nguyen",
                                        IsConfirm = true
                                    }); 
            model.Entity<IdentityUserRole<long>>().HasData(new IdentityUserRole<long>() {UserId = 1,RoleId =1 });
            model.Entity<RoomCategory>().HasData(new RoomCategory() { Id = 1, RoomCategoryName = "Nhà trọ" },
                            new RoomCategory() { Id = 2, RoomCategoryName = "Chung Cư Mini" },
                            new RoomCategory() { Id = 3, RoomCategoryName = "Nhà Nguyên Căn" },
                            new RoomCategory() { Id = 4, RoomCategoryName = "Chung cư" }
                            );
        }
    }
}
