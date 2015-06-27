using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class ScenarioDTO : BaseDTO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureObjectId { get; set; } 
        public FeatureDTO Feature { get; set; }
        public List<StatementDTO> Statements { get; set; }

        public ScenarioDTO()
        {
            Statements = new List<StatementDTO>();
        }
    }
}
