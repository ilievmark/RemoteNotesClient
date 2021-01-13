using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace RemoteNotes.DAL.MySql
{
    public class DatabaseContext
    {
        private IDbConnection connection;
        private bool ownConnection;
        private IDbTransaction transaction;

        private string connectionString;

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public DatabaseContext(string connectionString, bool ownConnection)
        {
            this.connectionString = connectionString;
            this.ownConnection = ownConnection;
        }

        public IDbCommand CreateCommand()
        {
            this.CheckConnection();
            this.CheckTransaction();

            var command = connection.CreateCommand();
            command.Transaction = transaction;
            return command;
        }

        public void AddCommand(IDbCommand command)
        {
            this.CheckConnection();
            this.CheckTransaction();

            command.Connection = connection;
            command.Transaction = transaction;
        }

        private void CheckConnection()
        {
            if (this.connection == null)
            {
                this.OpenConnection();
            }
        }

        private void CheckTransaction()
        {
            if (this.transaction == null)
            {
                this.StartTransaction();
            }
        }

        private void OpenConnection()
        {
            connection = new MySqlConnection(this.connectionString);
            connection.Open();
        }

        private void StartTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void SaveChanges()
        {
            if (transaction == null)
            {
                throw new InvalidOperationException("Transaction have already been already been commited. Check your transaction handling.");
            }

            transaction.Commit();
            transaction = null;
        }

        public void RejectChanges()
        {
            transaction.Rollback();
            transaction = null;
        }

        public void Dispose()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }

            if (this.connection != null && this.ownConnection)
            {
                this.connection.Close();
                this.connection = null;
            }
        }
    }
}