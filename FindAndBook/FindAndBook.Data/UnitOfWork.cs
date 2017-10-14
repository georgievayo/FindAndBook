using System;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;

namespace FindAndBook.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IFindAndBookContext dbContext;

        public UnitOfWork(IFindAndBookContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this.dbContext = dbContext;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
