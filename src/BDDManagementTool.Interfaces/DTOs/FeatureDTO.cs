using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class FeatureDTO : BaseDTO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public List<ScenarioDTO> Scenarios { get; set; }
        public List<StatementDTO> Background { get; set; }
    }
}