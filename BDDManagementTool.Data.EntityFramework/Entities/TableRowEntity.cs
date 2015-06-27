using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableRowEntity : BaseEntity, ITableRowEntity
    {
        public IList<ITableCellEntity> Cells { get; set; }

        public TableRowEntity()
        {
            Cells = new List<ITableCellEntity>();
        }
    }
}