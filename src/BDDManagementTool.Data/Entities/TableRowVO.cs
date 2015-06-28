using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableRowVO : BaseVO
    {
        public IList<TableCellVO> Cells { get; set; }
    }
}