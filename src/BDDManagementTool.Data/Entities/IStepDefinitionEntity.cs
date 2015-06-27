using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface IStepDefinitionEntity : IBaseEntity, IIdentifiable
    {
        Guid ObjectId { get; set; }
        string MethodName { get; set; }
        string MethodSignature { get; set; }
        int CountUsages { get; set; }
        IList<IStepDefinitionTypeEntity> StepDefinitionTypes { get; set; }
    }
}
