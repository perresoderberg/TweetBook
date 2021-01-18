//using System;
//using System.Data;

//namespace TweetBook.Core.Data
//{
//    public interface IUnitOfWork : IDisposable
//    {
//        IDbConnection Connection { get; }
//        Guid GuidId { get; }
//        IDbTransaction Transaction { get; }

//        void Commit();
//        void RollBack();
//    }
//}