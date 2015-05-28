using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class FilterRepoParamDTO
    {
        public SearchFilterType Type { get; set; }

        public string Text { get; set; }
    }
}