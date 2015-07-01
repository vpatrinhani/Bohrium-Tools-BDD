using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities
{
    public class FeatureRepository : BaseRepository
    {
        public FeatureRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Add(Feature entity)
        {
            UnitOfWork.Context.Set<Feature>().Add(entity);
        }
    }
}
