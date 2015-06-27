using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableColumnEntity : BaseEntity, ITableColumnEntity
    {
        public Guid ObjectId { get; set; }
        public string Value { get; set; }
    }
}