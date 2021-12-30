using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Hub
{
    public interface IHubMessager
    {
        Task<TResult> SendMessageAsync<TParam, TResult>(string messageTag, TParam param);
    }
}