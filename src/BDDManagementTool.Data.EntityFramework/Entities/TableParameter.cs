using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameter : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public Guid StatementId { get; set; }
        public virtual Statement Statement { get; set; }
        public virtual IList<TableColumn> Columns { get; set; }
        public virtual IList<TableRow> Rows { get; set; }
    }
}