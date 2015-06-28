using System.Data.Entity;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public BDDMgmtDbContext Context { get; private set; }

        public UnitOfWork(BDDMgmtDbContext context)
        {
            this.Context = context;
        }

        internal DbSet<T> GetDbSet<T>()
            where T : class
        {
            return Context.Set<T>();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}