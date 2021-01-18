//using System;
//using System.Data;
//using System.Data.Common;

//namespace TweetBook.Core.Data
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        protected readonly IPostRepository _postRepository;
//        protected readonly IDbConnection _connection;
//        protected IDbTransaction _transaction;
//        protected readonly Guid _guidId;
//        public UnitOfWork(IPostRepository postRepository, DbConnection dBConnection, DbTransaction dbTransaction)
//        {
//            _guidId = Guid.NewGuid();

//            _postRepository = postRepository;
//            _connection = dBConnection;
//            _transaction = dbTransaction;

//            if (_connection.State != ConnectionState.Open)
//                _connection.Open();
//            _transaction = _connection.BeginTransaction();
//        }
//        public virtual void Commit()
//        {
//            _transaction.Commit();
//            _transaction.Dispose();
//            _transaction = _connection.BeginTransaction();
//        }

//        public virtual void RollBack()
//        {
//            _transaction.Rollback();
//            _transaction.Dispose();
//            _transaction = _connection.BeginTransaction();
//        }

//        public virtual void Dispose()
//        {
//            if (_transaction != null)
//                _transaction.Dispose();
//            _transaction = null;

//        }

//        public IDbConnection Connection { get { return _connection; } }
//        public Guid GuidId { get { return _guidId; } }

//        public IDbTransaction Transaction { get { return _transaction; } }
//    }
//}
