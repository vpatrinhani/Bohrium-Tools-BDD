using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class StepDefinitionDTO : BaseDTO, IIdentifiable
    {
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public List<StepDefinitionTypeDTO> StepDefinitionTypes { get; set; }

        public StepDefinitionDTO()
        {
            StepDefinitionTypes = new List<StepDefinitionTypeDTO>();
        }
    }
}
