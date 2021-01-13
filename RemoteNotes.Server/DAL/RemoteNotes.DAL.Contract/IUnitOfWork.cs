using System;

namespace RemoteNotes.DAL.Contract
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();

        void Rollback();

        T GetRepository<T>();
    }
}