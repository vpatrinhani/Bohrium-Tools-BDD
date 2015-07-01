using System;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void CommitAsync();
    }
}