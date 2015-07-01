using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities
{
    public class ScenarioRepository : BaseRepository
    {
        public ScenarioRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Add(Scenario entity)
        {
            UnitOfWork.Context.Set<Scenario>().Add(entity);
        }
    }
}
