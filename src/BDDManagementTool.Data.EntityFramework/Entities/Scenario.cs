using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    [Table("Scenario")]
    public class Scenario : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        [NotMapped]
        public Guid FeatureObjectId { get; set; }
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
        public virtual IList<Statement> Statements { get; set; }
    }
}