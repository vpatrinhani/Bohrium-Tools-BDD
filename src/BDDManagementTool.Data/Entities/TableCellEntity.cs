using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableCellEntity : BaseEntity, IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Value { get; set; }
    }
}
