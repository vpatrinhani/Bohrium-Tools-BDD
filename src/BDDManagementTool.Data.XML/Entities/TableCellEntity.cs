using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableCellEntity : BaseEntity, ITableCellEntity
    {
        public Guid ObjectId { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}