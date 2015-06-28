using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableRow : BaseEntity
    {
        public IList<TableCell> Cells { get; set; }

        public TableRow()
        {
            Cells = new List<TableCell>();
        }
    }
}