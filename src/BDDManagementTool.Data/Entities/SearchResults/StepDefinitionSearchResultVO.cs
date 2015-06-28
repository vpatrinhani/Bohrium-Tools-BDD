using Bohrium.Tools.BDDManagementTool.Data.Infrastructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities.SearchResults
{
    public class StepDefinitionSearchResultVO : BaseSearchResultVO
    {
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
    }
}