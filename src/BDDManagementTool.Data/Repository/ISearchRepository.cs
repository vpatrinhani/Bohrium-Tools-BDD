using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Repository
{
    public interface ISearchRepository : IRepository
    {
        IEnumerable<BaseVO> Search(FilterRepoParam[] filters);

        FeatureVO GetFeatureById(Guid id);

        ScenarioVO GetScenarioById(Guid id);

        StepDefinitionVO GetStepDefinitionById(Guid id);
    }
}