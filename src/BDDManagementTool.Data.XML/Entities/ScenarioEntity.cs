using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class ScenarioEntity : BaseEntity, IScenarioEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureId { get; set; }
        public IFeatureEntity Feature { get; set; }
        public IList<IStatementEntity> Statements { get; set; }

        public ScenarioEntity()
        {
            Statements = new List<IStatementEntity>();
        }
    }
}