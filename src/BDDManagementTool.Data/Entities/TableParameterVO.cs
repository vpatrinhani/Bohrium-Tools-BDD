using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableParameterVO : BaseVO
    {
        public Guid Id { get; set; }
        public Guid StatementId { get; set; }
        public StatementVO Statement { get; set; }
        public IList<TableColumnVO> Columns { get; set; }
        public IList<TableRowVO> Rows { get; set; }
    }
}