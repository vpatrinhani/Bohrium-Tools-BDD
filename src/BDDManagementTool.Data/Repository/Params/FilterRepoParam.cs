using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;

namespace Bohrium.Tools.BDDManagementTool.Data.Repository.Params
{
    public class FilterRepoParam
    {
        public SearchFilterType Type { get; set; }

        public string Text { get; set; }
    }
}