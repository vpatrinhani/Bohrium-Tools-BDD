using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface ITableColumnEntity : IBaseEntity, IIdentifiable
    {
        Guid ObjectId { get; set; }
        string Value { get; set; }
    }
}
