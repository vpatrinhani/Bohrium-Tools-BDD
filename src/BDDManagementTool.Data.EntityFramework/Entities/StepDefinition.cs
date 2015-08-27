using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class StepDefinition : BaseEntity
    {
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public virtual ICollection<StepDefinitionType> StepDefinitionTypes { get; set; }
    }
}