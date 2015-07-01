using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class ScenarioVO : BaseVO, IIdentifiable, IDescriptable, ITaggable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureObjectId { get; set; }
        public FeatureVO Feature { get; set; }
        public IList<StatementVO> Statements { get; set; }
    }
}