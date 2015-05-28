using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;

namespace Bohrium.Tools.BDDManagementTool.Data.Repository
{
    public interface ISearchRepository
    {
        IEnumerable<BaseEntity> Search(FilterRepoParam[] filters);

        FeatureEntity GetFeatureById(Guid id);
        ScenarioEntity GetScenarioById(Guid id);
        StepDefinitionEntity GetStepDefinitionById(Guid id);
    }
}