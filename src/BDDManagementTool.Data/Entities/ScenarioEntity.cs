using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class ScenarioEntity : BaseEntity, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureId { get; set; } 
        public FeatureEntity Feature { get; set; }
        public List<StatementEntity> Statements { get; set; }

        public ScenarioEntity()
        {
            Statements = new List<StatementEntity>();
        }
    }
}
