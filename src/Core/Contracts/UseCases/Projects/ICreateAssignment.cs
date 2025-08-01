﻿using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Result;

namespace Core.Contracts.UseCases.Projects
{
    public interface ICreateAssignment
    {
        Task<Result<CreateAssignmentResponseDto>> ExecuteAsync(CreateAssignmentRequestDto requestDto, CancellationToken cancellationToken);
    }
}