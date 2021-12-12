using Xamarin.Forms;

namespace RemoteNotes.Domain.Contract.Navigation
{
    public interface IPageBuilder
    {
        Page BuildPage(string tag);
    }
}
