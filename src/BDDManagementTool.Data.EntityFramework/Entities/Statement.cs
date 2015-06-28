using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class Statement : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public virtual Scenario Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
        public Guid StepDefinitionId { get; set; }
        public virtual StepDefinition StepDefinition { get; set; }
        public virtual IList<StepDefinitionType> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public virtual IList<TableParameter> TableParameters { get; set; }
    }
}