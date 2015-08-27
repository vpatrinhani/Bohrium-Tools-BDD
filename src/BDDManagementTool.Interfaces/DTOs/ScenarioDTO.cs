using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class ScenarioDTO : BaseDTO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureId { get; set; }
        public FeatureDTO Feature { get; set; }
        public List<StatementDTO> Statements { get; set; }

        public ScenarioDTO()
        {
            Statements = new List<StatementDTO>();
        }
    }
}