using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RemoteNotes.BL.Security
{
    public class AuthOptions
    {
        private const string Key = "41389c1853a7012b761fd71ebd8c7f46f976d529";

        public const string Issuer = "RemoteNotesAPIServer";
        public const string Audience = "RemoteNotesClient";
        public const int TokenLifetimeMinutes = 2000;

        public static SymmetricSecurityKey SymmetricSecurityKey =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
