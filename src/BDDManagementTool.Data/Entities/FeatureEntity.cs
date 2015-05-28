using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class FeatureEntity : BaseEntity, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public List<ScenarioEntity> Scenarios { get; set; }
        public List<StatementEntity> Background { get; set; }
    }
}