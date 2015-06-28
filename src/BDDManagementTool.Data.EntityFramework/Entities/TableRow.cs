using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableRow : BaseEntity
    {
        public virtual IList<TableCell> Cells { get; set; }
    }
}