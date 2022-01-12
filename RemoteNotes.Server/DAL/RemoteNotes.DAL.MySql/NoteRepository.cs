using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RemoteNotes.DAL.Contract;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.MySql
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(ISqlDataManager sqlDataManager) : base(sqlDataManager)
        {
        }

        protected override void AddInputParameterCollection(IDbCommand sqlCommand, Note note)
        {
            _sqlDataManager.AddParameter(sqlCommand, "@Id", note.Id);
            _sqlDataManager.AddParameter(sqlCommand, "@UserId", note.UserId);
            _sqlDataManager.AddParameter(sqlCommand, "@Title", note.Title);
            _sqlDataManager.AddParameter(sqlCommand, "@Description", note.Description);
            _sqlDataManager.AddParameter(sqlCommand, "@CreatedAt", note.CreatedAt);
            _sqlDataManager.AddParameter(sqlCommand, "@ModifiedAt", DateTime.Now);
        }

        public Task<List<Note>> GetByUserIdAsync(int id)
        {
            string queryCommand = string.Format("select * from `{0}s`", _entityName);

            return Task.FromResult(_sqlDataManager.DoQuery<Note>(queryCommand));
        }
    }
}