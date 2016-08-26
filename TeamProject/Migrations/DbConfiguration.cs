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

    public sealed class DbConfiguration : DbMigrationsConfiguration<BlogDbContextEntities>
    {
        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlogDbContextEntities context)
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
                    UserRole = "Guest"
                };
                var commonUser = new DbUserConfiguration()
                {
                    User = "test@test.com",
                    Email = "test@email.com",
                    Password = "test@test.com",
                    Fullname = "Common user",
                    UserRole = "Member"
                };


                CreateUsers(context, adminUser);
                CreateUsers(context, pesho);
                CreateUsers(context, commonUser);
                CreateSeverealTestEvents(context);
            }

        }

        private void CreateSeverealTestEvents(BlogDbContextEntities context)
        {
            context.Posts.Add(new Post()
            {
                Title = "BFS",
                Body = "Breadth-first search (BFS) is an algorithm for traversing or searching tree or graph data structures. It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key'[1]) and explores the neighbor nodes first, before moving to the next level neighbors.",
                Description = "Some Description",
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
                Title = "Tallest",
                Body = "The heights of the tallest trees in the world have been the subject of considerable dispute and much exaggeration. Modern verified measurements with laser rangefinders, or with tape drop measurements made by tree climbers (such as those carried out by canopy researchers), have shown that some older measuring methods and measurements are often unreliable, sometimes producing exaggerations of 5% to 15% or more above the real height.[1] Historical claims of trees growing to 130 m (430 ft), and even 150 m (490 ft), are now largely disregarded as unreliable, and attributed to human error. https://en.wikipedia.org/wiki/List_of_superlative_trees",
                Description = "Some Description", 
                PostedOn = DateTime.Now.AddDays(5),
                Modified = DateTime.Now.AddDays(4),
                User = context.Users.OrderByDescending(e => e.Email).First(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() { Text ="Last User comment", User = context.Users.First()} ,
                    new Comment() { Text ="First User comment", User = context.Users.OrderByDescending(u => u.Id).First()}
                }
            });
            context.Posts.Add(new Post()
            {
                Title = "Olympic games",
                Body = "The Olympic Games, which originated in ancient Greece as many as 3,000 years ago, were revived in the late 19th century and have become the world’s preeminent sporting competition. From the 8th century B.C. to the 4th century A.D., the Games were held every four years in Olympia, located in the western Peloponnese peninsula, in honor of the god Zeus. The first modern Olympics took place in 1896 in Athens, and featured 280 participants from 13 nations, competing in 43 events. Since 1994, the Summer and Winter Olympic Games have been held separately and have alternated every two years.The first written records of the ancient Olympic Games date to 776 B.C., when a cook named Coroebus won the only event–a 192-meter footrace called the stade (the origin of the modern “stadium”)–to become the first Olympic champion. However, it is generally believed that the Games had been going on for many years by that time. Legend has it that Heracles (the Roman Hercules), son of Zeus and the mortal woman Alcmene, founded the Games, which by the end of the 6th century B.C had become the most famous of all Greek sporting festivals. The ancient Olympics were held every four years between August 6 and September 19 during a religious festival honoring Zeus. The Games were named for their location at Olympia, a sacred site located near the western coast of the Peloponnese peninsula in southern Greece. Their influence was so great that ancient historians began to measure time by the four-year increments in between Olympic Games, which were known as Olympiads.",
                PostedOn = DateTime.Now.AddDays(5),
                Modified = DateTime.Now.AddDays(4),
                User = context.Users.OrderByDescending(e => e.Email).First(),
                Comments = new HashSet<Comment>()
                {

                    new Comment() { Text ="Last User comment", User = context.Users.First()} ,
                    new Comment() { Text ="First User comment", User = context.Users.OrderByDescending(u => u.Id).First()}
                }
            });
            context.Posts.Add(new Post()
            {
                Title = "history of Formula One",
                Body = "Formula One (the formula in the name refers to a set of rules to which all participants and cars must comply and was originally and briefly known as Formula A) can trace its roots back to the earliest days of motor racing, and emerged from the buoyant European racing scene of the inter-war years. Plans for a Formula One drivers' championship were discussed in the late 1930s but were shelved with the onset of World War Two.In 1946 the idea was rekindled and in that season the first races were held and the following year the decision was made to launch a drivers' championship. It took until 1950 for the details to be hammered out and in May 1950 the first world championship race was held at Silverstone - the first F1 race had taken place a month earlier in Pau. Only seven of the twenty or so Formula One races that season counted towards the title but the championship was up and running. Even as more races were included in the championship, there were plenty of non-championship Formula One races. Non-championship races continued until 1983 when rising costs ruled them unprofitable.",
                Description = "Some Description",
                PostedOn = DateTime.Now.AddDays(5),
                Modified = DateTime.Now.AddDays(4),
                User = context.Users.OrderByDescending(e => e.Email).First(),
                Comments = new HashSet<Comment>()
                {

                    new Comment() { Text ="Last User comment", User = context.Users.First()} ,
                    new Comment() { Text ="First User comment", User = context.Users.OrderByDescending(u => u.Id).First()}
                }
            });
            context.Posts.Add(new Post()
            {
                Title = "Burj Dubai",
                Body = "Construction of the Burj Khalifa began in 2004, with the exterior completed in 2009. The primary structure is reinforced concrete. The building was opened in 2010 as part of a new development called Downtown Dubai. It is designed to be the centrepiece of large-scale, mixed-use development. The decision to build the building is reportedly based on the government's decision to diversify from an oil-based economy, and for Dubai to gain international recognition. The building was named in honour of the ruler of Abu Dhabi and president of the United Arab Emirates, Khalifa bin Zayed Al Nahyan; Abu Dhabi and the UAE government lent Dubai money to pay its debts. The building broke numerous height records, including its designation as the tallest tower in the world.Burj Khalifa was designed by Adrian Smith, then of Skidmore, Owings & Merrill (SOM), whose firm designed the Willis Tower and One World Trade Center. Hyder Consulting was chosen to be the supervising engineer with NORR Group Consultants International Limited chosen to supervise the architecture of the project. The design of Burj Khalifa is derived from patterning systems embodied in Islamic architecture, incorporating cultural and historical elements particular to the region, such as in the Great Mosque of Samarra. The Y-shaped plan is designed for residential and hotel usage. A buttressed core structural system is used to support the height of the building, and the cladding system is designed to withstand Dubai's summer temperatures. It contains a total of 57 elevators and 8 escalators.Critical reception to Burj Khalifa has been generally positive, and the building has received many awards. However, the labour issues during construction were controversial, since the building was built primarily by workers from South East Asia, who were allegedly treated poorly.",
                Description = "Some Description",
                PostedOn = DateTime.Now.AddDays(5),
                Modified = DateTime.Now.AddDays(4),
                User = context.Users.OrderByDescending(e => e.Email).First(),
                Comments = new HashSet<Comment>()
                {

                    new Comment() { Text ="Last User comment", User = context.Users.First()} ,
                    new Comment() { Text ="First User comment", User = context.Users.OrderByDescending(u => u.Id).First()}
                }
            });


        }

        private void CreateUsers(BlogDbContextEntities context, DbUserConfiguration currentUser)
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

