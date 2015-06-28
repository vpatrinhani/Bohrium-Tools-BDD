using System;

namespace Bohrium.Tools.BDDManagementTool.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}