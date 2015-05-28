using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableParameterEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid StatementId { get; set; }
        public StatementEntity Statement { get; set; }
        public List<TableColumnEntity> Columns { get; set; }
        public List<TableRowEntity> Rows { get; set; }

        public TableParameterEntity()
        {
            Columns = new List<TableColumnEntity>();
            Rows = new List<TableRowEntity>();
        }
    }
}