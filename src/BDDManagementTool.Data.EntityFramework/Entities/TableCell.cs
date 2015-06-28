using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableCell : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}