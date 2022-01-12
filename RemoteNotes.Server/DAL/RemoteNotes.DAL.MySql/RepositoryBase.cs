using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.MySql
{
    public abstract class RepositoryBase<T> : ICRUDRepository<T> where T : IIdentificable
    {
        protected readonly string _entityName;
        protected readonly ISqlDataManager _sqlDataManager;

        public RepositoryBase(ISqlDataManager sqlDataManager)
        {
            _sqlDataManager = sqlDataManager;
            _entityName = typeof(T).Name;
        }
        
        public Task<T> GetByIdAsync(int id)
        {
            string queryCommand = string.Format("Get{0}ById", _entityName);
            return _sqlDataManager.GetByIdAsync<T>(queryCommand, id);
        }

        public T GetById(int id)
        {
            string queryCommand = string.Format("Get{0}ById", _entityName);
            return _sqlDataManager.GetById<T>(queryCommand, id);
        }

        public virtual void Update(T element, bool commit = true)
        {
            string queryCommand = string.Format("Update{0}", _entityName);
            IDbCommand sqlCommand = this._sqlDataManager.GetCommand(queryCommand);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlDataManager.AddParameter(sqlCommand, @"Id", element.Id);
            AddInputParameterCollection(sqlCommand, element);
            _sqlDataManager.ExecuteCommand(sqlCommand, commit);
        }
        
        public virtual void Add(T element, bool commit = true)
        {
            string queryCommand = string.Format("Add{0}", _entityName);
            IDbCommand sqlCommand = _sqlDataManager.GetCommand(queryCommand);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            AddInputParameterCollection(sqlCommand, element);
            _sqlDataManager.AddParameter(sqlCommand, @"Id", 4, ParameterDirection.Output);
            _sqlDataManager.ExecuteCommand(sqlCommand, commit);
            var id = int.Parse(_sqlDataManager.GetValue(sqlCommand, "@Id").ToString());
            element.Id = id;
        }
        
        public async Task AddAsync(T element, bool commit = true)
        {
            string queryCommand = string.Format("Add{0}", _entityName);
            IDbCommand sqlCommand = _sqlDataManager.GetCommand(queryCommand);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            AddInputParameterCollection(sqlCommand, element);
            _sqlDataManager.AddParameter(sqlCommand, @"Id", 4, ParameterDirection.Output);
            await _sqlDataManager.ExecuteCommandAsync(sqlCommand, commit);
            var id = int.Parse(_sqlDataManager.GetValue(sqlCommand, "@Id").ToString());
            element.Id = id;
        }
        
        public void Delete(T element)
        {
            Delete(element.Id);
        }
        
        public void Delete(int id, bool commit = true)
        {
            string commandText = string.Format("Delete{0}", _entityName);
            IDbCommand sqlCommand = _sqlDataManager.GetCommand(commandText);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlDataManager.AddParameter(sqlCommand, @"Id", id);
            _sqlDataManager.ExecuteCommand(sqlCommand, commit);
        }

        public async Task DeleteAsync(int id, bool commit = true)
        {
            string commandText = string.Format("Delete{0}", _entityName);
            IDbCommand sqlCommand = _sqlDataManager.GetCommand(commandText);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlDataManager.AddParameter(sqlCommand, @"Id", id);
            await _sqlDataManager.ExecuteCommandAsync(sqlCommand, commit);
        }
        
        public virtual List<T> GetCollection()
        {
            string queryCommand = string.Format("select * from `{0}s`", _entityName);
            return DoQuery(queryCommand);
        }

        public void Clear(bool commit = true)
        {
            string queryCommand = string.Format("delete from `{0}s`", _entityName);
            _sqlDataManager.ExecuteCommand(queryCommand, true);
        }
        
        public List<T> GetCollection(int topNumber)
        {
            string queryCommand = string.Format("select top {0} from `{1}s`", topNumber, _entityName);
            return DoQuery(queryCommand);
        }
        
        protected List<T> DoQuery(string queryCommand, Dictionary<string, object> parameterCollection)
        {
            return _sqlDataManager.DoQuery<T>(queryCommand, parameterCollection);
        }
        
        protected List<T> DoQuery(string queryCommand)
        {
            return _sqlDataManager.DoQuery<T>(queryCommand);
        }

        protected abstract void AddInputParameterCollection(IDbCommand sqlCommand, T element);
    }
}