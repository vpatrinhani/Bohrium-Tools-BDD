using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.XML
{
    public class UnitOfWork : IUnitOfWork
    {
        public XmlDataContext Context { get; private set; }

        public UnitOfWork(XmlDataContext context)
        {
            this.Context = context;
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
        }
    }
}
