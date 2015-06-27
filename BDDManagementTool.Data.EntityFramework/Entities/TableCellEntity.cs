using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableCellEntity : BaseEntity, ITableCellEntity
    {
        public Guid ObjectId { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}