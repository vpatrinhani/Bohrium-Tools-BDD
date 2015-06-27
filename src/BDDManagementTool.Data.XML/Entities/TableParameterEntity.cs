using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableParameterEntity : BaseEntity, ITableParameterEntity
    {
        public Guid Id { get; set; }
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