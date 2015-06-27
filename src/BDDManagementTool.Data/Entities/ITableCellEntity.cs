using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface ITableCellEntity : IBaseEntity, IIdentifiable
    {
        Guid ObjectId { get; set; }
        Guid ColumnId { get; set; }
        string Value { get; set; }
    }
}
