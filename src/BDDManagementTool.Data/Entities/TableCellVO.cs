using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableCellVO : BaseVO, IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}