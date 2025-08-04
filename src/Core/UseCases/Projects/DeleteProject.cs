using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using System.Net;

namespace Core.UseCases.Projects;
public class DeleteProject : IDeleteProject
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        Project? project = await _unitOfWork.ProjectRepository.GetByIdWithAllChildrenAsync(projectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        project.SoftDelete();

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if(!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.NoContent);
    }
}