using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer
{
    public class DataInitializer
    {
        // private static DatabaseEntities _context;
        public DataInitializer(DatabaseEntities context)
        {
            // _context = context;
            // Seed(context);
        }
        public static void SeedData(DatabaseEntities context)
        {
            RoleList(context);
            UserList(context);
            UserInRoles(context);
            Gender(context);
        }

        public static void RoleList(DatabaseEntities context)
        {
            context.Role.AddOrUpdate(x => x.Id,
                new Roles() { Id = 1, RoleId = new Guid("C7F5D260-0AB7-4FF5-AE99-6DD3F4031F23"), RoleName = "Anonymous", RoleFriendlyName = "Anonymous" },
                new Roles() { Id = 2, RoleId = new Guid("87B0BB0B-6083-40F3-A3D5-E8EFCB1A2E68"), RoleName = "Admin", RoleFriendlyName = "Admin" },
                new Roles() { Id = 3, RoleId = new Guid("12a75c42-faf0-489f-98bd-c7ebc079adc7"), RoleName = "Employee", RoleFriendlyName = "Employee" }
                );
        }

        public static void UserList(DatabaseEntities context)
        {
            context.User.AddOrUpdate(
                x => x.Id,
                 new User()
                 {
                     Id = 1,
                     UserId = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                     FirstName = "Admin",
                     LastName = "Admin",
                     UserName = "SuperAdmin",
                     Password = "/D5K5mOWxrfuLFwmmwS1Ww==",
                     PasswordFormat = 3,
                     PasswordSalt = "",
                     Email = "adminsupport@yopmail.com",
                     IsActive = true,
                     CreatedOn = DateTime.UtcNow,
                     CreatedBy = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                     UpdatedOn = DateTime.UtcNow,
                     IsModified = false,
                     LastLoginDate = DateTime.UtcNow,
                     IsDeleted = false
                 },
                  new User()
                  {
                      Id = 2,
                      UserId = new Guid("5D4A8986-52C1-4B0D-96B6-27F96946140D"),
                      FirstName = "Employee",
                      LastName = "Employee",
                      UserName = "Employee",
                      Password = "/D5K5mOWxrfuLFwmmwS1Ww==",
                      PasswordFormat = 3,
                      PasswordSalt = "",
                      Email = "Employee@yopmail.com",
                      IsActive = true,
                      CreatedOn = DateTime.UtcNow,
                      CreatedBy = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                      UpdatedOn = DateTime.UtcNow,
                      IsModified = false,
                      LastLoginDate = DateTime.UtcNow,
                      IsDeleted = false
                  }
                );
        }

        public static void UserInRoles(DatabaseEntities context)
        {
            context.UserInRoles.AddOrUpdate(x => x.Id, new UserInRoles()
            {
                Id = 1,
                RoleId = new Guid("87B0BB0B-6083-40F3-A3D5-E8EFCB1A2E68"),
                UserId = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                CreateBy = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                CreatedOn = DateTime.UtcNow
            },
            new UserInRoles()
            {
                Id = 2,
                RoleId = new Guid("12a75c42-faf0-489f-98bd-c7ebc079adc7"),
                UserId = new Guid("5D4A8986-52C1-4B0D-96B6-27F96946140D"),
                CreateBy = new Guid("D93D6E81-5133-47BF-8728-60316CC85CE4"),
                CreatedOn = DateTime.UtcNow
            }
            );
        }


        public static void Gender(DatabaseEntities context)
        {
            context.Genders.AddOrUpdate(x => x.Id,
                new Gender() { Id = 1, Name = "Male", CreatedOn = DateTime.UtcNow, CreatedBy = new Guid("5D4A8986-52C1-4B0D-96B6-27F96946140D"), IsDeleted = false },
               new Gender() { Id = 2, Name = "Female", CreatedOn = DateTime.UtcNow, CreatedBy = new Guid("5D4A8986-52C1-4B0D-96B6-27F96946140D"), IsDeleted = false },
                 new Gender() { Id = 3, Name = "Others", CreatedOn = DateTime.UtcNow, CreatedBy = new Guid("5D4A8986-52C1-4B0D-96B6-27F96946140D"), IsDeleted = false }
                );
        }

    }
}
