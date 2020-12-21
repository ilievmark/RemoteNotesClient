using System.Threading.Tasks;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract.Hub;
using RemoteNotes.UI.ViewModel.Abstract;
using RemoteNotes.UI.ViewModel.Service;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModel
{
    public class LoginPageViewModel : BaseViewModel, IInitialize
    {
        private readonly INavigationController _navigationController;
        private readonly IHubConnection _hubConnection;

        public LoginPageViewModel(
            INavigationController navigationController,
            IHubConnection hubConnection)
        {
            _navigationController = navigationController;
            _hubConnection = hubConnection;
        }
        
        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        
        public ICommand LoginCommand => new Command(CreateCommandHandler(OnLoginCommand));

        private void OnLoginCommand()
        {
        }
        
        public Task InitializeAsync() => _hubConnection.ConnectAsync();
    }
}