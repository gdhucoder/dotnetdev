using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace NLayerWebApiDemo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ICatRepository _catRepository;
        private IDogRepository _dogRepository;
        private bool _disposed;

        public UnitOfWork(IDbConnectionFactory factory)
        {
            // Server=127.0.0.1;Port=5004;Uid=root;Password=password;Database=testdb
            _connection = factory.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ICatRepository CatRepository
        {
            get { return _catRepository ?? (_catRepository = new CatRepository(_transaction)); }
        }


        public IDogRepository DogRepository
        {
            get { return _dogRepository ?? (_dogRepository = new DogRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _catRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
