﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.infrastructure.Data
{
    public class EmployeesDapperContext : IDapperContext
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private string _connectionString;
        

        public EmployeesDapperContext(IConfiguration configuration)
        {
            _connectionString = $"Server={configuration.GetConnectionString("EmployeeDB:HostName")};" +
                                $"Database={configuration.GetConnectionString("EmployeeDB:Catalogue")};" +
                                $"User Id={configuration.GetConnectionString("EmployeeDB:User")};" +
                                $"Password={configuration.GetConnectionString("EmployeeDB:Password")};" +
                                $"MultipleActiveResultSets=True;";
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection is null) _connection = new SqlConnection(_connectionString);
                if(IsConnectionClosed()) _connection.Open();
                return _connection;
            }
        }

        public bool IsConnectionClosed() => _connection != null && _connection.State.Equals(ConnectionState.Closed);

        public bool HasOpenConnection() => !IsConnectionClosed();

        public bool HasOpenTransaction() => _transaction != null && _transaction.Connection != null;

        public IDbTransaction BeginTransaction()
        {
            if(!HasOpenTransaction()) _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void ClearTransaction()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
                ClearTransaction();
            }
            catch
            {
                if (HasOpenTransaction()) Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (!HasOpenConnection())
            {
                _connection.Close();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
                ClearTransaction();
            }
            catch
            {
                throw;
            }
        }

        private T ExecuteQueryTransaction<T>(Func<IDbTransaction, T> query,IDbTransaction transaction)
        {
            var transactionResult = query(transaction);
            Commit();
            return transactionResult;

        }

        public T Transation<T>(Func<IDbTransaction, T> query)
        {
            using var connection = Connection;
            using var transaction = connection.BeginTransaction();
            try
            {
                return ExecuteQueryTransaction(query,transaction);
            }
            catch
            {
                Rollback();
                throw;
            }
        }
    }
}
