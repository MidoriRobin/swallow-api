using Swallow.Models;
using Swallow.Models.Responses;

namespace Swallow.Authorization;
    public interface IJwtAuth
    {
        AuthenticateResponse Authentication(User userInfo, string ipAddress);
        int? Validate(string token);
        RefreshToken GenerateRefreshToken(string ipAddress);
        void RevokeRefreshToken(string token);
}


