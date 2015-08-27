using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameterRow : BaseEntity
    {
        public Guid TableParameterId { get; set; }
        public virtual ICollection<TableParameterCell> Cells { get; set; }
    }
}