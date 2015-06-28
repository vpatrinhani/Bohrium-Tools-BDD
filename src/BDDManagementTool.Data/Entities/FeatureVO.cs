using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class FeatureVO : BaseVO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public IList<ScenarioVO> Scenarios { get; set; }
        public IList<StatementVO> Background { get; set; }
    }
}