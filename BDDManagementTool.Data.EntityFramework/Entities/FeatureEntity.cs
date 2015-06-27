using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    [Table("Feature")]
    public class FeatureEntity : BaseEntity, IFeatureEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public IList<IScenarioEntity> Scenarios { get; set; }
        public IList<IStatementEntity> Background { get; set; }
    }
}