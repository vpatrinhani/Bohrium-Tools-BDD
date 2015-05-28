using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableRowEntity : BaseEntity
    {
        public List<TableCellEntity> Cells { get; set; }

        public TableRowEntity()
        {
            Cells = new List<TableCellEntity>();
        }
    }
}
