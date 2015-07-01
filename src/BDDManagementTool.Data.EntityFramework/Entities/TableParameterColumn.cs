using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameterColumn : BaseEntity
    {
        public Guid TableParameterId { get; set; }
        public string Label { get; set; }
        public string CellType { get; set; }

        public TableParameterColumn()
        {
            CellType = typeof(object).FullName;
        }
    }
}