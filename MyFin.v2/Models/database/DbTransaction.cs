using Microsoft.EntityFrameworkCore.Storage;
using MyFin.v2.Models.Entities.database;

namespace MyFin.v2.Models.database
{
    public class DbTransaction : IDisposable
    {
        private IDbContextTransaction dbContextTransaction;
        private FinContext finContext;

        public DbTransaction(FinContext finContext)
        {
            this.finContext = finContext;
        }

        public IDbContextTransaction begin()
        {
            dbContextTransaction = finContext.Database.BeginTransaction();
            return dbContextTransaction;
        }

        public void Commit()
        {
            dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            //dbContextTransaction.Dispose();
        }
    }
}
