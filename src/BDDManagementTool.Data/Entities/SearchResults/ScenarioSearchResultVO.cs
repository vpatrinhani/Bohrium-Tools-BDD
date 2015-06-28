using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrastructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities.SearchResults
{
    public class ScenarioSearchResultVO : BaseSearchResultVO, ITaggable
    {
        public string[] Tags { get; set; }
        public int StatementsCount { get; set; }
    }
}