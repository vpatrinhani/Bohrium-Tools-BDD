using System;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StepDefinitionTypeEntity : BaseEntity, IIdentifiable, ITypeable
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinitionEntity StepDefinition { get; set; }
        public string RegexStatement { get; set; }
        public int CountUsages { get; set; }
    }
}
