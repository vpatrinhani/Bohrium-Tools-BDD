using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    [Table("Scenario")]
    public class ScenarioEntity : BaseEntity, IScenarioEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        [NotMapped]
        public Guid FeatureObjectId { get; set; }
        public IFeatureEntity Feature { get; set; }
        public IList<IStatementEntity> Statements { get; set; }

        public long FeatureId { get; set; }

        public ScenarioEntity()
        {
            Statements = new List<IStatementEntity>();
        }
    }
}