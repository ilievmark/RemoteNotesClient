using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RemoteNotes.DAL.Contract
{
    public interface ISqlDataManager
    {
        void ExecuteCommand(string queryCommand, bool commit);

        IDbCommand GetCommand(string commandText);

        void ExecuteCommand(IDbCommand command, bool commit);

        T GetById<T>(string query, Guid id);

        T GetByQueryParameter<T>(string query, string parameterName, string parameter);

        List<T> ExecuteReader<T>(IDbCommand command);

        List<T> GetCollection<T>(string tableName);

        List<T> DoQuery<T>(string query);

        Task<List<T>> DoQueryAsync<T>(string query);

        List<T> DoQuery<T>(string query, Dictionary<string, object> parameterCollection);

        Task<List<T>> DoQueryAsync<T>(string query, Dictionary<string, object> parameterCollection);

        void AddParameter(IDbCommand sqlCommand, string parameterName, object value, ParameterDirection direction = ParameterDirection.Input);

        object GetValue(IDbCommand sqlCommand, string parameterName);

        Task<T> GetByIdAsync<T>(string queryCommand, Guid id);

        Task ExecuteCommandAsync(IDbCommand sqlCommand, bool commit);
    }
}