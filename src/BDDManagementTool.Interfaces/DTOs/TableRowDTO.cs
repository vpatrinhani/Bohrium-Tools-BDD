using System.Collections.Generic;

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