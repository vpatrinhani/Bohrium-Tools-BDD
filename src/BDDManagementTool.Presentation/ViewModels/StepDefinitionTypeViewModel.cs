using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.Presentation.ViewModels
{
    public class StepDefinitionTypeViewModel : BaseSearchResultViewModel
    {
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Regex Expression")]
        public string RegexExpression { get; set; }

        [Display(Name = "Usages")]
        public int CountUsages { get; set; }
    }
}