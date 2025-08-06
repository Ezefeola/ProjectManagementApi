using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Common;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Utilities.Mappers;
using System.Net;

namespace Core.UseCases.Assignments;
public class GetAssignmentsForProject : IGetAssignmentsForProject
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAssignmentsForProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetAssignmentsForProjectResponseDto>> ExecuteAsync(
        ProjectId projectId,
        GetAssignmentsForProjectRequestDto parametersRequestDto,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<Assignment> assignments = await _unitOfWork.AssignmentRepository.GetAssignmentsForProjectAsync(
            projectId,
            parametersRequestDto,
            cancellationToken
        );
        if (!assignments.Any())
        {
            return Result<GetAssignmentsForProjectResponseDto>.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.AssignmentErrors.ASSIGNMENTS_NOT_FOUND]);
        }

        int totalAssignmentsForProject = await _unitOfWork.AssignmentRepository.CountByProjectIdAsync(projectId, cancellationToken);

        GetAssignmentsForProjectResponseDto responseDto = assignments.ToGetAssignmentsForProjectResponseDto(
            parametersRequestDto,
            totalAssignmentsForProject
        );
        return Result<GetAssignmentsForProjectResponseDto>.Success(HttpStatusCode.OK)
                                                          .WithPayload(responseDto);
    }
}