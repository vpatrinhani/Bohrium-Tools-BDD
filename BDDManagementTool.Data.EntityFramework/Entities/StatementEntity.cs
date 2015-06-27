using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class StatementEntity : BaseEntity, IStatementEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public IScenarioEntity Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public IFeatureEntity Feature { get; set; }
        public Guid StepDefinitionId { get; set; }
        public IStepDefinitionEntity StepDefinition { get; set; }
        public IList<IStepDefinitionTypeEntity> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public IList<ITableParameterEntity> TableParameters { get; set; }

        public StatementEntity()
        {
            StepDefinitionTypes = new List<IStepDefinitionTypeEntity>();
            TableParameters = new List<ITableParameterEntity>();
        }
    }
}