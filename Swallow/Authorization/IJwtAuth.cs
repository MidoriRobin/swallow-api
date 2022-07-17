using Swallow.Models;

namespace Swallow.Authorization;
    public interface IJwtAuth
    {
        string Authentication(User userInfo);
        int? Validate(string token);
    }


