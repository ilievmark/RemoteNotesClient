using System.Threading.Tasks;
using RemoteNotes.Domain;
using RemoteNotes.Domain.Requests;
using RemoteNotes.Domain.Response;

namespace RemoteNotes.API.Contract
{
    public interface IAuthorizationApi
    {
        Task<ApiResponse<AuthorizationResponse>> SignInAsync(SignInRequest request);
        
        Task<ApiResponse<AuthorizationResponse>> SignUpAsync(SignUpRequest request);

        Task<ApiResponse<AuthorizationResponse>> UpdateAuthorizationAsync();
    }
}