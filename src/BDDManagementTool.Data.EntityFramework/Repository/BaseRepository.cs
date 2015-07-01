using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository
{
    public abstract class BaseRepository
    {
        protected UnitOfWork UnitOfWork { get; private set; }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork as UnitOfWork;
            // TODO: Typed exception
            if (UnitOfWork == null) throw new Exception("Must be an UnitOfWork for EntityFramework");
        }
    }
}
