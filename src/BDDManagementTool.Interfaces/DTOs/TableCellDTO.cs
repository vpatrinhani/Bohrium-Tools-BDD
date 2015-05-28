using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableCellDTO : BaseDTO, IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}
