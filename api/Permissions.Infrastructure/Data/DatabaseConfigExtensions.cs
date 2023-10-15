using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Permissions.Domain.Models;
using Permissions.Domain.Repositories;
using Permissions.Infrastructure.DataAccess;
using Permissions.Infrastructure.DataAccess.Repository;

namespace Permissions.Infrastructure.Data;

public static class DatabaseConfigExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PermissionsContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        
        services.AddTransient(typeof(IRepository<Permission>), typeof(PermissionsRepository));
        services.AddTransient(typeof(IRepository<PermissionType>), typeof(PermissionTypeRepository));

        return services;
    }
}