using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.Assignments
{
    public interface ICreateAssignment
    {
        Task<Result<CreateAssignmentResponseDto>> ExecuteAsync(CreateAssignmentRequestDto requestDto, CancellationToken cancellationToken);
    }
}