using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableParameter : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid StatementId { get; set; }
        public Statement Statement { get; set; }
        public IList<TableColumn> Columns { get; set; }
        public IList<TableRow> Rows { get; set; }

        public TableParameter()
        {
            Columns = new List<TableColumn>();
            Rows = new List<TableRow>();
        }
    }
}