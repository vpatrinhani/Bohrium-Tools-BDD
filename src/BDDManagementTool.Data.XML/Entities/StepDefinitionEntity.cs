using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class StepDefinitionEntity : BaseEntity, IStepDefinitionEntity
    {
        public Guid ObjectId { get; set; }
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public IList<IStepDefinitionTypeEntity> StepDefinitionTypes { get; set; }

        public StepDefinitionEntity()
        {
            StepDefinitionTypes = new List<IStepDefinitionTypeEntity>();
        }
    }
}