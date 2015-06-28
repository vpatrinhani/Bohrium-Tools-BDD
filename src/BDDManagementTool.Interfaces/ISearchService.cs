using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<BaseDTO> Search(string text);

        IEnumerable<BaseDTO> Search(SearchFilterType filterType, string text);

        FeatureDTO GetFeatureById(Guid id);

        ScenarioDTO GetScenarioById(Guid id);

        StepDefinitionDTO GetStepDefinitionById(Guid id);
    }
}