using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class FeatureEntity : BaseEntity, IFeatureEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public IList<IScenarioEntity> Scenarios { get; set; }
        public IList<IStatementEntity> Background { get; set; }
    }
}