using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities
{
    public class StepDefinitionRepository : BaseRepository
    {
        public StepDefinitionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Add(StepDefinition entity)
        {
            UnitOfWork.Context.Set<StepDefinition>().Add(entity);
        }
    }
}
