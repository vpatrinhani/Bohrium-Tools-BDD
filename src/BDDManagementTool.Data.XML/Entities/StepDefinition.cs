using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class StepDefinition : BaseEntity
    {
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public IList<StepDefinitionType> StepDefinitionTypes { get; set; }

        public StepDefinition()
        {
            StepDefinitionTypes = new List<StepDefinitionType>();
        }
    }
}