using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.WebApp.ViewModels
{
    public class FeatureSearchResultViewModel : BaseSearchResultViewModel, ITaggableViewModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public string[] Tags { get; set; }

        [Display(Name = "Scenarios")]
        public List<ScenarioSearchResultViewModel> Scenarios { get; set; }

        [Display(Name = "Backgrounds")]
        public List<StatementSearchResultViewModel> Background { get; set; }
    }
}