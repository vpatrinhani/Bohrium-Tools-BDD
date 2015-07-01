using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class StepDefinitionTypeDTO : BaseDTO, IIdentifiable, ITypeable
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinitionDTO StepDefinition { get; set; }
        public string RegexExpression { get; set; }
        public int CountUsages { get; set; }
    }
}