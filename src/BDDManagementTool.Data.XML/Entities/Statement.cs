using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class Statement : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public Scenario Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinition StepDefinition { get; set; }
        public IList<StepDefinitionType> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public IList<TableParameter> TableParameters { get; set; }

        public Statement()
        {
            StepDefinitionTypes = new List<StepDefinitionType>();
            TableParameters = new List<TableParameter>();
        }
    }
}