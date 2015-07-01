using System;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUnityContainer container;

        public BDDMgmtDbContext Context { get; private set; }

        public UnitOfWork(IUnityContainer container, BDDMgmtDbContext context)
        {
            this.container = container;
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

        public async void CommitAsync()
        {
            await Context.SaveChangesAsync();
        }


        public void Dispose()
        {
            Context.Dispose();

            if (container != null)
            {
                var registration = container.Registrations.FirstOrDefault(a => a.RegisteredType == typeof(BDDMgmtDbContext));

                if (registration != null)
                {
                    registration.LifetimeManager.RemoveValue();
                }
            }
        }
    }
}