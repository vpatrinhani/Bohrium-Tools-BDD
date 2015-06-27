using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableColumnEntity : BaseEntity, ITableColumnEntity
    {
        public Guid ObjectId { get; set; }
        public string Value { get; set; }
    }
}