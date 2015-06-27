using System;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface IStepDefinitionTypeEntity : IBaseEntity, IIdentifiable, ITypeable
    {
        Guid ObjectId { get; set; }
        string Type { get; set; }
        Guid StepDefinitionId { get; set; }
        IStepDefinitionEntity StepDefinition { get; set; }
        string RegexStatement { get; set; }
        int CountUsages { get; set; }
    }
}
