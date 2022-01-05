using System.Threading.Tasks;
using RemoteNotes.Domain.Models;

namespace RemoteNotes.BL.Contract.Authorization
{
    public interface IAuthorizationService
    {
        Task<TokenModel> SignUpAsync(string username, string password);
        
        Task<TokenModel> SignInAsync(string username, string password);
        
        Task<TokenModel> RefreshTokenAsync(string refreshToken);
    }
}