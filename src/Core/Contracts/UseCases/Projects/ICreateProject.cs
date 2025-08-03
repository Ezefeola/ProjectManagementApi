using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.Projects
{
    public interface ICreateProject
    {
        Task<Result<CreateProjectResponseDto>> ExecuteAsync(CreateProjectRequestDto requestDto, CancellationToken cancellationToken);
    }
}