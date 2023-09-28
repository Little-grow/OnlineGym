using OnlineGym.Authentication.Models;

namespace OnlineGym.Authentication.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);

        Task<AuthModel> LoginAsync(TokenRequestModel model);

        Task<string> AddRoleAsync(AddRoleModel role);
    }
}

