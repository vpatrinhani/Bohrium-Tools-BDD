using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.Presentation.ViewModels
{
    public class ScenarioSearchResultViewModel : BaseSearchResultViewModel, ITaggableViewModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public string[] Tags { get; set; }

        [Display(Name = "Feature")]
        public FeatureSearchResultViewModel Feature { get; set; }

        [Display(Name = "Statements")]
        public List<StatementSearchResultViewModel> Statements { get; set; }
    }
}