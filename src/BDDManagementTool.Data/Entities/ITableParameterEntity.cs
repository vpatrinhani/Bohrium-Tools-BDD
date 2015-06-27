using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface ITableParameterEntity : IBaseEntity
    {
        Guid ObjectId { get; set; }
        Guid StatementId { get; set; }
        IStatementEntity Statement { get; set; }
        IList<ITableColumnEntity> Columns { get; set; }
        IList<ITableRowEntity> Rows { get; set; }
    }
}