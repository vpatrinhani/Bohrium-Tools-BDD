using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StatementEntity : BaseEntity, IIdentifiable, ITypeable, IDescriptable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public ScenarioEntity Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public FeatureEntity Feature { get; set; }
        public Guid StepDefinitionId { get; set; } 
        public StepDefinitionEntity StepDefinition { get; set; }
        public List<StepDefinitionTypeEntity> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public List<TableParameterEntity> TableParameters { get; set; }

        public StatementEntity()
        {
            StepDefinitionTypes = new List<StepDefinitionTypeEntity>();
            TableParameters = new List<TableParameterEntity>();
        }
    }
}