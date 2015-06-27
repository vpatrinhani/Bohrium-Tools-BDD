using System;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class StepDefinitionTypeDTO : BaseDTO, IIdentifiable, ITypeable
    {
        public Guid ObjectId { get; set; }
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinitionDTO StepDefinition { get; set; }
        public string RegexStatement { get; set; }
        public int CountUsages { get; set; }
    }
}
