using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class Step : BaseEntity
    {
        public string Type { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public Guid? StepDefinitionId { get; set; }
        public virtual StepDefinition StepDefinition { get; set; }
        public Guid? ScenarioId { get; set; }
        public virtual Scenario Scenario { get; set; }
        public Guid? FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
        [NotMapped]
        public string MultilineTextParameter { get; set; }
        [NotMapped]
        public virtual IList<StepDefinitionType> StepDefinitionTypes { get; set; }
        public virtual IList<TableParameter> TableParameters { get; set; }
    }
}