using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameter : BaseEntity
    {
        public Guid StepId { get; set; }
        public virtual Step Step { get; set; }
        public virtual IList<TableParameterColumn> Columns { get; set; }
        public virtual IList<TableParameterRow> Rows { get; set; }
    }
}