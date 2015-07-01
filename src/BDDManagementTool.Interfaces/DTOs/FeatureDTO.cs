using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class FeatureDTO : BaseDTO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public List<ScenarioDTO> Scenarios { get; set; }
        public List<StatementDTO> Background { get; set; }
    }
}