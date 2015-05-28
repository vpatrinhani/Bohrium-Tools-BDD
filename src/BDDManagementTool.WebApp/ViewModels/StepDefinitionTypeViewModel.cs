using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.WebApp.ViewModels
{
    public class StepDefinitionTypeViewModel : BaseSearchResultViewModel
    {
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "RegexStatement")]
        public string RegexStatement { get; set; }

        [Display(Name = "Usages")]
        public int CountUsages { get; set; }
    }
}