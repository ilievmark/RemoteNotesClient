using System;
using System.Collections.Generic;
using RemoteNotes.DAL.Contract;
using RemoteNotes.DAL.MySql;

namespace RemoteNotes.DAL.IntegrationTest.Context
{
    /// <summary>
    /// The testing context.
    /// </summary>
    public class TestingContext
    {
        /// <summary>
        /// The dal manager factory.
        /// </summary>
        private static IUnitOfWork unitOfWork;

        static TestingContext()
        {
            var connectionString =
                ConnectionStringReader.GetConnectionString(databaseName:"Notes", xmlFilePath:"Configuration/connectionStrings.config");
            var databaseContext = new DatabaseContext(connectionString, true);
            var sqlDataManager = new SqlDataManager(databaseContext);

            unitOfWork = new UnitOfWork(databaseContext, new Dictionary<Type, object>
            {
                { typeof(UserRepository), new UserRepository(sqlDataManager) },
                { typeof(NoteRepository), new NoteRepository(sqlDataManager) }
            });
        }

        /// <summary>
        /// The get dal manager factory.
        /// </summary>
        /// <returns>
        /// The <see cref="IDalManagerFactory"/>.
        /// </returns>
        public static IUnitOfWork GetUnitOfWork()
        {
            return unitOfWork;
        }
    }
}
