using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableCellDTO : BaseDTO, IIdentifiable
    {
        public Guid ObjectId { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}