using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using News.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole 
                {
                    Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC"),
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "luongvietdung1212@gmail.com",
                    NormalizedEmail = "luongvietdung1212@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Dung",
                    LastName = "Luong",
                    DoB = new DateTime(1997, 12, 12)
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC"),
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE")
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                   Id = 1,
                   Title = "bán đất nền q9",
                   Local = "quận 9",
                   Description = "gần chợ, gần trường",
                   Price = "20tr/m2",
                   DateCreated = new DateTime(2020, 01, 01),
                   Status = true
                });

            modelBuilder.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    ImagePath = "0bf08e07060b.jpg",
                    Caption = "hinh1",
                    DateCreated = new DateTime(2020, 01, 01),
                    IsDefault = true,
                    ProductId = 1
                });
        }
    }
}
