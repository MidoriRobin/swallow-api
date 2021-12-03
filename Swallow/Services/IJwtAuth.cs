namespace Swallow.Services;
    public interface IJwtAuth
    {
        string Authentication(string email, string password);
    }


