using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StepDefinitionTypeVO : BaseVO, IIdentifiable, ITypeable
    {
        public Guid ObjectId { get; set; }
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinitionVO StepDefinition { get; set; }
        public string RegexStatement { get; set; }
        public int CountUsages { get; set; }
    }
}