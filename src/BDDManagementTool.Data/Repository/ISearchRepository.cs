using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;

namespace Bohrium.Tools.BDDManagementTool.Data.Repository
{
    public interface ISearchRepository
    {
        IEnumerable<IBaseEntity> Search(FilterRepoParam[] filters);

        IFeatureEntity GetFeatureById(Guid id);
        IScenarioEntity GetScenarioById(Guid id);
        IStepDefinitionEntity GetStepDefinitionById(Guid id);
    }
}