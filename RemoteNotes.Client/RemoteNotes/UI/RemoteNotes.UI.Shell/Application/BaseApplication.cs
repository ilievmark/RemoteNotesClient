using System.Threading.Tasks;
using Autofac;
using RemoteNotes.Domain.Contract.Navigation;

namespace RemoteNotes.UI.Shell.Application
{
    public abstract class BaseApplication : Xamarin.Forms.Application
    {
        protected IContainer Container;
        
        public virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
        }

        public abstract Task SetupNavigationAsync(INavigationService navigationService);

        public void SetContainer(IContainer container)
        {
            Container = container;
        }
    }
}
