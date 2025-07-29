using Capitan360.Application.Services.Permission.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.Identity;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Domain.Repositories.User;
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
        UserManager<User> userManager, IPermissionService permissionService,
        IUserPermissionVersionControlRepository userPermissionVersionControlRepository,
        ICompanyPreferencesRepository companyPreferencesRepository,
        IUserProfileRepository profileRepository, IUserCompanyRepository userCompanyRepository
        , ICompanyRepository companyRepository) : IPrimaryInformationSeeder

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
                        CompanyType = 1,
                        Active = true
                    };

                    var result = await userManager.CreateAsync(user, phoneNumbers[i]);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                        // addUserControl Version
                        await userPermissionVersionControlRepository.SetUserPermissionVersion(user.Id, cancellationToken);

                        var company = new Company()
                        {
                            CompanyTypeId = 1,
                            Name = "شرکت پژواک",
                            Code = "123456",
                            PhoneNumber = "05112345678",
                            Active = false,
                            IsParentCompany = true,
                            CountryId = 1,
                            ProvinceId = 12,
                            CityId = 183,
                            Description = "شرکت اصلی و پایه سیستم است"
                        };

                        await companyRepository.CreateCompanyAsync(company, cancellationToken);
                        var profile = new UserProfile()
                        {
                            UserId = user.Id
                        };
                        var userCompany =
                            new UserCompany()
                            {
                                CompanyId = company.Id,
                                UserId = user.Id
                            };
                        var companyPreferences = new CompanyPreferences()
                        {
                            CompanyId = company.Id,
                            EconomicCode = "123456789",
                            NationalId = "123456789",
                            RegistrationId = "123456789",
                            CaptainCargoName = "SampleName",
                            CaptainCargoCode = "123456789"
                        };
                        await profileRepository.CreateUserProfile(profile, cancellationToken);
                        await userCompanyRepository.Create(userCompany, cancellationToken);
                        await companyPreferencesRepository.CreateCompanyPreferencesAsync(companyPreferences,
                            cancellationToken);
                    }
                    else
                    {
                        throw new Exception("Error In Seeding User");
                    }

                    i++;
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            //TODO:Log Errors of InsertData of Identity
        }
    }
}