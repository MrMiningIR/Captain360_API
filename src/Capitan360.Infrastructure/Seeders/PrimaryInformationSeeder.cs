using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Capitan360.Infrastructure.Seeders
{
    public interface IPrimaryInformationSeeder
    {
        Task SeedDataAsync(CancellationToken cancellationToken);
    }

    internal class PrimaryInformationSeeder(ApplicationDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager) : IPrimaryInformationSeeder
    {

        public async Task SeedDataAsync(CancellationToken cancellationToken)
        {
            // Apply any pending migrations
            await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
            if (!await dbContext.Database.CanConnectAsync(cancellationToken))
            {
                throw new Exception("Can't connect to the database");

            }


            // Default Permissions
            var permissions = new List<string>
            {
                "ViewUsers", "EditUsers", "DeleteUsers",
                "CreateRoles", "AssignRoles",
                "ViewProducts", "EditProducts", "DeleteProducts"
            };

            // Seed Permissions
            foreach (var permissionName in permissions)
            {
                if (!dbContext.Permissions.Any(p => p.Name == permissionName))
                {
                    dbContext.Permissions.Add(new Permission { Name = permissionName });
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            // Default Groups with Permissions
            var groupsWithPermissions = new Dictionary<string, List<string>>
            {
                {
                    "AdminGroup", new List<string>
                    {
                        "ViewUsers", "EditUsers", "DeleteUsers",
                        "CreateRoles", "AssignRoles",
                        "ViewProducts", "EditProducts", "DeleteProducts"
                    }
                },
                {
                    "EditorGroup", new List<string>
                    {
                        "ViewUsers", "EditUsers",
                        "ViewProducts", "EditProducts"
                    }
                },
                {
                    "ViewerGroup", new List<string>
                    {
                        "ViewProducts"
                    }
                }
            };

            // Seed Groups with Permissions
            foreach (var groupEntry in groupsWithPermissions)
            {
                var groupName = groupEntry.Key;
                var groupPermissions = groupEntry.Value;

                if (!dbContext.Groups.Any(g => g.Name == groupName))
                {
                    var group = new Group { Name = groupName };
                    dbContext.Groups.Add(group);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    var allPermissions = await dbContext.Permissions.ToListAsync(cancellationToken);
                    foreach (var permissionName in groupPermissions)
                    {
                        var permission = allPermissions.FirstOrDefault(p => p.Name == permissionName);
                        if (permission != null)
                        {
                            dbContext.GroupPermissions.Add(new GroupPermission
                            {
                                GroupId = group.Id,
                                PermissionId = permission.Id
                            });
                        }
                    }
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            // Default Roles with Groups
            var rolesWithGroups = new Dictionary<string, List<string>>
            {
                { "SuperAdmin", new List<string> { "AdminGroup" } },
                { "Manager", new List<string> { "EditorGroup" } },
                { "User", new List<string> { "ViewerGroup" } }
            };
           

            // Seed Roles with Groups
            foreach (var roleEntry in rolesWithGroups)
            {
                var roleName = roleEntry.Key;
                var groupNames = roleEntry.Value;

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    await roleManager.CreateAsync(role);

                    foreach (var groupName in groupNames)
                    {
                        var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Name == groupName, cancellationToken: cancellationToken);
                        if (group != null)
                        {
                            dbContext.RoleGroups.Add(new RoleGroup
                            {
                                RoleId = role.Id,
                                GroupId = group.Id
                            });
                        }
                    }
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            // Default Users with Roles
            var usersWithRoles = new Dictionary<string, string>
            {
                { "superadmin", "SuperAdmin" },
                { "manager", "Manager" },
                { "user", "User" }
            };
            List<string>phoneNumbers = new List<string> { "09155165067", "09155165068", "09155165069" };
            int i = 0;
            // Seed Users with Roles
            foreach (var userEntry in usersWithRoles)
            {
                var username = userEntry.Key;
                var roleName = userEntry.Value;
               
                var dbUser = await userManager.FindByNameAsync(username);
                if (dbUser == null)
                {
                    var user = new User
                    {
                        UserName = username,
                        PhoneNumber = phoneNumbers[i],
                        Email = $"{username}@example.com",
                        FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username)
                    };

                    var result = await userManager.CreateAsync(user, phoneNumbers[i]);
                  //  var result = await userManager.CreateAsync(user, $"{username}@123A");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }

                    var group = await dbContext.Groups
                        .Include(g => g.RoleGroups)
                        .ThenInclude(rg => rg.Role)
                        .FirstOrDefaultAsync(g => g.RoleGroups.Any(rg => rg.Role.Name == roleName), cancellationToken);


                    await dbContext.UserGroups.AddAsync(new UserGroup() { User = user, GroupId =group!.Id  }, cancellationToken);
                    i++;
                }
            }

            //TODO:Log Errors of InsertData of Identity



            //if (!await dbContext.Users.AnyAsync(cancellationToken))
            //{
            //    await SeedUsersAsync(dbContext, cancellationToken);
            //}




        }
    }
}
