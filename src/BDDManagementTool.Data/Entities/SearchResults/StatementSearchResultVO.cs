using Bohrium.Tools.BDDManagementTool.Data.Infrastructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities.SearchResults
{
    public class StatementSearchResultVO : BaseSearchResultVO
    {
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid StepDefinitionId { get; set; }
        public ScenarioVO Scenario { get; set; }
        public StepDefinitionVO StepDefinition { get; set; }
    }
}