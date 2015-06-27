using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class StatementDTO : BaseDTO, IIdentifiable, ITypeable, IDescriptable
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public ScenarioDTO Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public FeatureDTO Feature { get; set; }
        public Guid StepDefinitionId { get; set; } 
        public StepDefinitionDTO StepDefinition { get; set; }
        public List<StepDefinitionTypeDTO> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public List<TableParameterDTO> TableParameters { get; set; }

        public StatementDTO()
        {
            StepDefinitionTypes = new List<StepDefinitionTypeDTO>();
            TableParameters = new List<TableParameterDTO>();
        }
    }
}