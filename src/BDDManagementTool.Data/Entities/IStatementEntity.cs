using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface IStatementEntity : IBaseEntity, IIdentifiable, ITypeable, IDescriptable
    {
        Guid ObjectId { get; set; }
        string Description { get; set; }
        string Type { get; set; }
        string Keyword { get; set; }
        Guid ScenarioId { get; set; }
        IScenarioEntity Scenario { get; set; }
        Guid FeatureId { get; set; }
        IFeatureEntity Feature { get; set; }
        Guid StepDefinitionId { get; set; } 
        IStepDefinitionEntity StepDefinition { get; set; }
        IList<IStepDefinitionTypeEntity> StepDefinitionTypes { get; set; }
        string MultilineTextParameter { get; set; }
        IList<ITableParameterEntity> TableParameters { get; set; }
    }
}