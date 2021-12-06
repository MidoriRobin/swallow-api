namespace Swallow.Authorization;
    public interface IJwtAuth
    {
        string Authentication(string email, string password);
        bool Validate(string token);
    }


