using Core.Contracts.Models;
using Core.Contracts.Repositories;

namespace Core.Contracts.UnitOfWork;
public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IProjectRepository ProjectRepository { get; }
    public IAssignmentRepository AssignmentRepository { get; }
    public IProjectCollaboratorRepository ProjectCollaboratorRepository { get; }

    public Task<SaveResult> CompleteAsync(CancellationToken cancellationToken = default);
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}