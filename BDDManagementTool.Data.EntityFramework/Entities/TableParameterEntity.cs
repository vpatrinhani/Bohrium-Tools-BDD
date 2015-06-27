using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameterEntity : BaseEntity, ITableParameterEntity
    {
        public Guid ObjectId { get; set; }
        public Guid StatementId { get; set; }
        public IStatementEntity Statement { get; set; }
        public IList<ITableColumnEntity> Columns { get; set; }
        public IList<ITableRowEntity> Rows { get; set; }

        public TableParameterEntity()
        {
            Columns = new List<ITableColumnEntity>();
            Rows = new List<ITableRowEntity>();
        }
    }
}