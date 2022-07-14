using Swallow.Models;

namespace Swallow.Authorization;
    public interface IJwtAuth
    {
        string Authentication(User userInfo);
        bool Validate(string token);
    }


