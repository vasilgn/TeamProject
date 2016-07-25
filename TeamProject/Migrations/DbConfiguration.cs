using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TeamProject.DataModels;

namespace TeamProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbConfiguration : DbMigrationsConfiguration<TeamProject.DataModels.ApplicationDbContext>
    {
        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeamProject.DataModels.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUserName = adminEmail;
                var adminPassWord = adminEmail;
                var adminFullName = "System Adminnistrator";
                string adminRole = "Administrator";

                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassWord, adminRole);
                CreateSeverealTestEvents(context);
            }
        }

        private void CreateSeverealTestEvents(ApplicationDbContext context)
        {
            context.Posts.Add(new Post()
            {
                Body = "Breadth-first search (BFS) is an algorithm for traversing or searching tree or graph data structures. It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key'[1]) and explores the neighbor nodes first, before moving to the next level neighbors.",
                PostDate = DateTime.Now.AddDays(5),
                Author = context.Users.First()
            });

            context.Posts.Add(new Post()
            {
                Body = "Test test Test test",
                PostDate = DateTime.Now.AddDays(-2),
                Comments = new HashSet<Comment>()
                {
                    new Comment() { Text ="<Anonymus> comment", },
                    new Comment() { Text ="User comment", Author = context.Users.First()}
                }
            });
        }

    private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminUserName, string adminFullName, string adminPassWord, string adminRole)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                FullName = adminFullName,
                Email = adminEmail
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManeger = new UserManager<ApplicationUser>(userStore);
            userManeger.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            var userCreateResult = userManeger.Create(adminUser, adminPassWord);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            var roleManeger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleCreateResult = roleManeger.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            var addAdminRoleResult = userManeger.AddToRole(adminUser.Id, adminRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

    }
}

