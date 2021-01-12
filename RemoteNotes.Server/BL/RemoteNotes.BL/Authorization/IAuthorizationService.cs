using System.Threading.Tasks;
using RemoteNotes.Domain.Security;

namespace RemoteNotes.BL.Authorization
{
    public interface IAuthorizationService
    {
        Task<TokenModel> SignUpAsync(string username, string password);
        Task<TokenModel> LogInAsync(string username, string password);
        Task<TokenModel> RefreshTokenAsync(string refreshToken);
    }
}