using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities
{
    public class StepRepository : BaseRepository
    {
        public StepRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Add(Step entity)
        {
            UnitOfWork.Context.Set<Step>().Add(entity);
        }
    }
}
