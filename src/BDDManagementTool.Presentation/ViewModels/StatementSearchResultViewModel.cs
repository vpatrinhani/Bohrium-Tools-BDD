using System;
using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.Presentation.ViewModels
{
    public class StatementSearchResultViewModel : BaseSearchResultViewModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Keyword")]
        public string Keyword { get; set; }

        [Display(Name = "Scenario")]
        public Guid ScenarioId { get; set; }

        [Display(Name = "Scenario")]
        public string ScenarioDescription { get; set; }

        [Display(Name = "Multiline Text Parameter")]
        public string MultilineTextParameter { get; set; }

        [Display(Name = "Tables")]
        public TableViewModel[] Tables { get; set; }

        [Display(Name = "StepDefinition")]
        public Guid StepDefinitionId { get; set; }

        [Display(Name = "Step Definition Method Name")]
        public string StepDefinitionMethodName { get; set; }
    }

    public class TableViewModel
    {
        public TableColumnViewModel[] Columns { get; set; }

        public TableRowViewModel[] Rows { get; set; }
    }

    public class TableColumnViewModel
    {
        public Guid Id { get; set; }

        public string Label { get; set; }
    }

    public class TableRowViewModel
    {
        public TableCellViewModel[] Cells { get; set; }
    }

    public class TableCellViewModel
    {
        public Guid ColumnId { get; set; }

        public string Value { get; set; }
    }
}