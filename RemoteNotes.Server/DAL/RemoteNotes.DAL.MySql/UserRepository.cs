using System.Data;
using System.Threading.Tasks;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.MySql
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISqlDataManager sqlDataManager) : base(sqlDataManager)
        {
        }

        public Task<User> GetUserAsync(string username)
        {
            string queryCommand = string.Format("Get{0}ByName", _entityName);
            return Task.FromResult(_sqlDataManager.GetByQueryParameter<User>(queryCommand, "Username", username));
        }

        protected override void AddInputParameterCollection(IDbCommand sqlCommand, User note)
        {
            _sqlDataManager.AddParameter(sqlCommand, "@Id", note.Id);
            _sqlDataManager.AddParameter(sqlCommand, "@Username", note.UserName);
            _sqlDataManager.AddParameter(sqlCommand, "@EncryptedPassword", note.EncryptedPassword);
        }
    }
}