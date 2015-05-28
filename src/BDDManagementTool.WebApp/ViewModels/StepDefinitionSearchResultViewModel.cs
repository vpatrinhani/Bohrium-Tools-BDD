using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.WebApp.ViewModels
{
    public class StepDefinitionSearchResultViewModel : BaseSearchResultViewModel
    {
        [Display(Name = "Method")]
        public string MethodName { get; set; }

        [Display(Name = "Signature")]
        public string MethodSignature { get; set; }

        [Display(Name = "Usages")]
        public int CountUsages { get; set; }

        [Display(Name = "Step Definition Types")]
        public List<StepDefinitionTypeViewModel> StepDefinitionTypes { get; set; }
    }
}