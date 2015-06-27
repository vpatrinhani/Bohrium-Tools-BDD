using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
