using Capitan360.Application.Services.Permission.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

namespace Capitan360.Infrastructure.Seeders
{
    public interface IPrimaryInformationSeeder
    {
        Task SeedDataAsync(CancellationToken cancellationToken, Assembly assembly);
    }

    internal class PrimaryInformationSeeder(ApplicationDbContext dbContext,
        IUnitOfWork unitOfWork,
        RoleManager<Role> roleManager,
        UserManager<User> userManager, IPermissionService permissionService, IUserPermissionVersionControlRepository userPermissionVersionControlRepository) : IPrimaryInformationSeeder

    {

        public async Task SeedDataAsync(CancellationToken cancellationToken, Assembly assembly)
        {



            var isDataBaseReady = await dbContext.Database.CanConnectAsync(cancellationToken);
            if (!isDataBaseReady)
                throw new SystemException("Database is not Ready, Check SqlServer Or Docker!");

            var pendingMigrates = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            if (pendingMigrates.Any())
                throw new SystemException("There are Some Migrations , need to Migrate!");

            if (!await dbContext.Database.CanConnectAsync(cancellationToken))
            {
                throw new Exception("Can't connect to the database");

            }

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            // Default Permissions
            #region MyRegion
            //var permissions = new List<string>
            //{

            //    ConstantNames.ViewUsers, ConstantNames.EditUsers, ConstantNames.DeleteUsers,
            //    ConstantNames.CreateRoles, ConstantNames.AssignRoles,
            //    ConstantNames.ViewProducts, ConstantNames.EditProducts, ConstantNames.DeleteProducts
            //}; 
            #endregion

            var permissions = permissionService.GetSystemPermissions(assembly);


            if (!permissions.Success || permissions.Data == null || !permissions.Data.Any())
                throw new SystemException("there are not any Permissions");

            // Seed Permissions
            await permissionService.DeleteUnAvailablePermissions(permissions.Data, cancellationToken);
            Console.WriteLine("DeleteUnAvailablePermissions is Done");
            await permissionService.SavePermissionsInSystem(permissions.Data, cancellationToken);
            Console.WriteLine("SavePermissionsInSystem is Done");

            var superAdminRole = await roleManager.FindByIdAsync("bcde120b-62bf-4268-b51d-ef55faffce4d");

            var dbPermissions = await permissionService.GetDbPermissionsId(cancellationToken);



            #region MyRegion
            // Default Groups with Permissions
            //var groupsWithPermissions = new Dictionary<string, List<string>>
            //{
            //    {
            //        ConstantNames.AdminGroup, [
            //            ConstantNames.ViewUsers, ConstantNames.EditUsers, ConstantNames.DeleteUsers,
            //            ConstantNames.CreateRoles, ConstantNames.AssignRoles,
            //            ConstantNames.ViewProducts, ConstantNames.EditProducts, ConstantNames.DeleteProducts
            //        ]
            //    },
            //    {
            //        ConstantNames.EditorGroup, [
            //            ConstantNames.ViewUsers, ConstantNames.EditUsers,
            //            ConstantNames.ViewProducts, ConstantNames.EditProducts
            //        ]
            //    },
            //    {
            //        ConstantNames.ViewerGroup, [ConstantNames.ViewProducts]
            //    }
            //};

            // Seed Groups with Permissions
            //foreach (var groupEntry in groupsWithPermissions)
            //{
            //    var groupName = groupEntry.Key;
            //    var groupPermissions = groupEntry.Value;

            //    if (!dbContext.Groups.Any(g => g.Name == groupName))
            //    {
            //        var group = new Group { Name = groupName };
            //        dbContext.Groups.Add(group);
            //        await dbContext.SaveChangesAsync(cancellationToken);

            //        var allPermissions = await dbContext.Permissions.ToListAsync(cancellationToken);
            //        foreach (var permissionName in groupPermissions)
            //        {
            //            var permission = allPermissions.FirstOrDefault(p => p.Name == permissionName);
            //            if (permission != null)
            //            {
            //                dbContext.GroupPermissions.Add(new GroupPermission
            //                {
            //                    GroupId = group.Id,
            //                    PermissionId = permission.Id
            //                });
            //            }
            //        }
            //        await dbContext.SaveChangesAsync(cancellationToken);
            //    }
            //}

            // Default Roles with Groups
            //var rolesWithGroups = new Dictionary<string, List<string>>
            //{
            //    { ConstantNames.SuperAdminRole, [ConstantNames.AdminGroup] },

            //    { ConstantNames.ManagerRole, [ConstantNames.EditorGroup] },
            //    { ConstantNames.UserRole, [ConstantNames.ViewerGroup] }
            //};


            // Seed Roles with Groups
            //foreach (var roleEntry in rolesWithGroups)
            //{
            //    var roleName = roleEntry.Key;
            //    var groupNames = roleEntry.Value;

            //    if (!await roleManager.RoleExistsAsync(roleName))
            //    {
            //        var role = new Role { Name = roleName };
            //        await roleManager.CreateAsync(role);

            //        foreach (var groupName in groupNames)
            //        {
            //            var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Name == groupName, cancellationToken: cancellationToken);
            //            if (group != null)
            //            {
            //                dbContext.RoleGroups.Add(new RoleGroup
            //                {
            //                    RoleId = role.Id,
            //                    GroupId = group.Id
            //                });
            //            }
            //        }
            //        await dbContext.SaveChangesAsync(cancellationToken);
            //    }
            //} 



            // Default Roles with Groups
            //var rolesWithGroups = new Dictionary<string, List<string>>
            //{
            //    { ConstantNames.SuperAdminRole, [ConstantNames.AdminGroup] },

            //    { ConstantNames.ManagerRole, [ConstantNames.EditorGroup] },
            //    { ConstantNames.UserRole, [ConstantNames.ViewerGroup] }
            //};


            #endregion
            // Seed Roles with Groups

            if (superAdminRole is null || superAdminRole.Name!.ToLower() != ConstantNames.SuperAdminRole.ToLower())
                throw new SystemException("There are not any Roles in Db or, could not find SuperAdminRole");

            if (!dbPermissions.Success || dbPermissions.Data == null || !dbPermissions.Data.Any())
                throw new SystemException("there are not any Permissions in Db");


            foreach (var permissionId in dbPermissions.Data)
            {
                var existRolePermission = await dbContext.RolePermissions
                    .AnyAsync(x => x.PermissionId == permissionId && x.RoleId == superAdminRole.Id, cancellationToken: cancellationToken);
                if (!existRolePermission)

                    dbContext.RolePermissions.Add(new RolePermission()
                    {
                        RoleId = superAdminRole.Id,
                        PermissionId = permissionId
                    });


            }
            await dbContext.SaveChangesAsync(cancellationToken);





            // Default Users with Roles
            var usersWithRoles = new Dictionary<string, string>
            {
                { ConstantNames.SamplePhone1, superAdminRole.Name! },
                //{ ConstantNames.ManagerUser, ConstantNames.ManagerRole },
                //{ ConstantNames.User, ConstantNames.UserRole }
            };
            List<string> phoneNumbers = [ConstantNames.SamplePhone1, ConstantNames.SamplePhone2, ConstantNames.SamplePhone3];
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
                        UserName = phoneNumbers[i],
                        PhoneNumber = phoneNumbers[i],
                        Email = $"{username}@sample.com",
                        FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username),
                        UserKind = (int)UserKind.Special,
                        CompanyType = 0
                    };

                    var result = await userManager.CreateAsync(user, phoneNumbers[i]);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                        // addUserControl Version
                        await userPermissionVersionControlRepository.SetUserPermissionVersion(user.Id, cancellationToken);
                    }

                    //var group = await dbContext.Groups
                    //    .Include(g => g.RoleGroups)
                    //    .ThenInclude(rg => rg.Role)
                    //    .FirstOrDefaultAsync(g => g.RoleGroups.Any(rg => rg.Role.Name == roleName), cancellationToken);


                    //await dbContext.UserGroups.AddAsync(new UserGroup() { User = user, GroupId = group!.Id }, cancellationToken);
                    i++;
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            //TODO:Log Errors of InsertData of Identity


        }


    }
}
