using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class Feature : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
        [NotMapped]
        public string[] Tags { get; set; }
        [NotMapped]
        public virtual ICollection<Step> Background { get; set; }
    }
}