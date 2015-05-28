using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableRowDTO : BaseDTO
    {
        public List<TableCellDTO> Cells { get; set; }

        public TableRowDTO()
        {
            Cells = new List<TableCellDTO>();
        }
    }
}
