using System;
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
        
        public Task<T> GetByIdAsync(Guid id)
        {
            string queryCommand = string.Format("Get{0}ById", _entityName);
            return _sqlDataManager.GetByIdAsync<T>(queryCommand, id);
        }

        public async Task AddAsync(T element, bool commit = true)
        {
            string queryCommand = string.Format("Add{0}", _entityName);
            IDbCommand sqlCommand = _sqlDataManager.GetCommand(queryCommand);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            AddInputParameterCollection(sqlCommand, element);
            _sqlDataManager.AddParameter(sqlCommand, @"Id", 4, ParameterDirection.Output);
            await _sqlDataManager.ExecuteCommandAsync(sqlCommand, commit);
            var id = Guid.Parse(_sqlDataManager.GetValue(sqlCommand, "@Id").ToString());
            element.Id = id;
        }

        protected abstract void AddInputParameterCollection(IDbCommand sqlCommand, T element);
    }
}