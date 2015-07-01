using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class TableParameterCell : BaseEntity
    {
        public Guid TableParameterColumnId { get; set; }
        public virtual TableParameterColumn TableParameterColumn { get; set; }
        public Guid TableParameterRowId { get; set; }
        public virtual TableParameterRow TableParameterRow { get; set; }
        public string Value { get; set; }
    }
}