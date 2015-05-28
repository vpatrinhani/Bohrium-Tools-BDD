using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableParameterDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public Guid StatementId { get; set; }
        public StatementDTO Statement { get; set; }
        public List<TableColumnDTO> Columns { get; set; }
        public List<TableRowDTO> Rows { get; set; }

        public TableParameterDTO()
        {
            Columns = new List<TableColumnDTO>();
            Rows = new List<TableRowDTO>();
        }
    }
}