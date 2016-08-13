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

    public sealed class DbConfiguration : DbMigrationsConfiguration<TeamProject.DataModels.BlogDbContext>
    {
        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeamProject.DataModels.BlogDbContext context)
        {

            if (!context.Users.Any())
            {
                var adminUser = new DbUserConfiguration()
                {
                    User = "admin@admin.com",
                    Email = "admin@admin.com",
                    Password = "admin@admin.com",
                    Fullname = "System Administrator",
                    UserRole = "Administrator"
                };
                var pesho = new DbUserConfiguration()
                {
                    User = "pesho@admin.com",
                    Email = "pesho@admin.com",
                    Password = "1234",
                    Fullname = "Pesho Pesev",
                    UserRole = "Members"
                };
                var commonUser = new DbUserConfiguration()
                {
                    User = "test@test.com",
                    Email = "test@email.com",
                    Password = "test@test.com",
                    Fullname = "Common user",
                    UserRole = "Members"
                };


                CreateUsers(context, adminUser);
                CreateUsers(context, pesho);
                CreateUsers(context, commonUser);
                CreateSeverealTestEvents(context);
            }

        }

        private void CreateSeverealTestEvents(BlogDbContext context)
        {
            context.Posts.Add(new Post()
            {
                Title = "BFS",
                Body = "Breadth-first search (BFS) is an algorithm for traversing or searching tree or graph data structures. It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key'[1]) and explores the neighbor nodes first, before moving to the next level neighbors.",
                Description = "Some Description",
                PostLikeCounter = 2,
                PostedOn = DateTime.Now.AddDays(5),
                Modified = DateTime.Now.AddDays(4),
                User = context.Users.OrderByDescending(e => e.Id).First(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() { Text ="Last User comment", User = context.Users.First()} ,
                    new Comment() { Text ="First User comment", User = context.Users.OrderByDescending(u => u.Id).First()}
                }
            });

            context.Posts.Add(new Post()
            {
                Title = "Testing Title",
                Body = "Test Brum",
                Description = "Some Description",
                PostLikeCounter = 5,
                PostedOn = DateTime.Now.AddDays(-2),
                User = context.Users.First(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() { Text ="<Anonymus> comment", User = context.Users.First()},
                    new Comment() { Text ="User comment", }
                }
            });


        }

        private void CreateUsers(BlogDbContext context, DbUserConfiguration currentUser)
        {
            var user = new ApplicationUser
            {
                UserName = currentUser.User,
                FullName = currentUser.Fullname,
                Email = currentUser.Email
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

            var userCreateResult = userManeger.Create(user, currentUser.Password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            var roleManeger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleCreateResult = roleManeger.Create(new IdentityRole(currentUser.UserRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            var addAdminRoleResult = userManeger.AddToRole(user.Id, currentUser.UserRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }
    }
}

