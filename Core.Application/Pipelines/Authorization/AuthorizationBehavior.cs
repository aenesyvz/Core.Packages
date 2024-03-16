﻿using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Contants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Extensions;

namespace Core.Application.Pipelines.Authorization;


public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        ICollection<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.GetRoleClaims();

        if (userRoleClaims == null)
            throw new AuthorizationException("You are not authenticated.");

        bool isNotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .FirstOrDefault(userRoleClaim =>
                userRoleClaim == GeneralOperationClaims.Admin || request.Roles.Contains(userRoleClaim)
            )
            .IsNullOrEmpty();
        if (isNotMatchedAUserRoleClaimWithRequestRoles)
            throw new AuthorizationException("You are not authorized.");

        TResponse response = await next();
        return response;
    }
}
