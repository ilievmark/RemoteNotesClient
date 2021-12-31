using System;
using RemoteNotes.UI.ViewModel.Authorization;

namespace RemoteNotes.UI.Shell.Linking
{
    public class ViewModelAssemblyIncluder
    {
        public Type LinkAssembly()
        {
            return typeof(LoginViewModel);
        }
    }
}