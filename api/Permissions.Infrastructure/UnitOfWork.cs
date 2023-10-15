using Permissions.Domain.Dto;
using Permissions.Domain.Events;
using Permissions.Domain.Models;
using Permissions.Domain.Repositories;
using Permissions.Infrastructure.DataAccess;

namespace Permissions.Infrastructure;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    public IRepository<Permission> PermissionsRepository { get; }
    public IRepository<PermissionType> PermissionTypesRepository { get; }

    private readonly IPublisher<PermissionEvent> _publisher;
    
    private readonly PermissionsContext _context;

    public UnitOfWork(PermissionsContext context, IRepository<Permission> permissionsRepository, IRepository<PermissionType> permissionTypesRepository, IPublisher<PermissionEvent> publisher)
    {
        _context = context;
        PermissionsRepository = permissionsRepository;
        PermissionTypesRepository = permissionTypesRepository;
        _publisher = publisher;
    }
    
    public Task Commit()
    {
        return _context.SaveChangesAsync();
    }

    public Task EmitEvent(EventType type)
    {
        var id = Guid.NewGuid().ToString();
        var permissionEvent = new PermissionEvent(id, type);

        return _publisher.PublishMessageAsync(permissionEvent);
    }

    public Task SyncWithElastic(Permission newPermission)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}