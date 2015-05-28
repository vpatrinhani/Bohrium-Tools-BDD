using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StepDefinitionEntity : BaseEntity, IIdentifiable
    {
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public List<StepDefinitionTypeEntity> StepDefinitionTypes { get; set; }

        public StepDefinitionEntity()
        {
            StepDefinitionTypes = new List<StepDefinitionTypeEntity>();
        }
    }
}
