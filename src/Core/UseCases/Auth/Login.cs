using Core.Contracts.DTOs.Auth.Request;
using Core.Contracts.DTOs.Auth.Response;
using Core.Contracts.Result;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Auth;
using Core.Domain.Users;
using Core.Services.Token;
using Core.Utilities.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Core.UseCases.Auth;
public class Login : ILogin
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<LoginRequestDto> _validator;

    public Login(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IPasswordHasher<User> passwordHasher,
        IValidator<LoginRequestDto> validator
    )
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<Result<LoginResponseDto>> ExecuteAsync(
        LoginRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validatorResult = _validator.Validate(requestDto);
        if (!validatorResult.IsValid)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([.. validatorResult.Errors.Select(e => e.ErrorMessage)]);
        }

        User? user = await _unitOfWork.UserRepository.GetByEmailAsync(requestDto.Email, cancellationToken);
        if (user is null)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([ValidationMessages.Auth.INVALID_CREDENTIALS]);
        }

        //PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, requestDto.Password);
        //if (passwordVerificationResult != PasswordVerificationResult.Success)
        //{
        //    return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
        //                                   .WithErrors([ValidationMessages.Auth.INVALID_CREDENTIALS]);
        //}
        if(user.Password != requestDto.Password)
        {
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                                           .WithErrors([ValidationMessages.Auth.INVALID_CREDENTIALS]);
        }

        LoginResponseDto responseDto = new()
        {
            Token = _tokenService.GenerateToken(user)
        };

        return Result<LoginResponseDto>.Success(HttpStatusCode.OK)
                                       .WithPayload(responseDto);
    }
}