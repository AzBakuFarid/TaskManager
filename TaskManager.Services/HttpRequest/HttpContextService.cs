using Microsoft.AspNetCore.Http;
using System;
using TaskManager.Domain.Entites;
using TaskManager.Services.Exceptions;

namespace TaskManager.Services.HttpRequest
{
    public interface IHttpContextService
    {
        User GetCurrentUserAndEnsureIsNotNull();
        User TryGetCurrentUser();

    }
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrentUserAndEnsureIsNotNull()
        {
            User user;
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new OperationNotAllowedException();
            }
            try
            {
                user = (User)_httpContextAccessor.HttpContext.Items["User"];
            }
            catch (Exception ex)
            {
                throw new OperationNotAllowedException(ex.Message);
            }
            return user;
        }
        public User TryGetCurrentUser()
        {
            User user = null;
            try
            {
               user = GetCurrentUserAndEnsureIsNotNull();
            }
            catch (OperationNotAllowedException){ }

            return user;
        }
    }
}
