using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class Scenario : BaseEntity
    {
        public string Description { get; set; }
        public Guid FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
        public virtual ICollection<Step> Statements { get; set; }
        [NotMapped]
        public string[] Tags { get; set; }
    }
}