using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableColumnDTO : BaseDTO, IIdentifiable
    {
        public Guid ObjectId { get; set; }
        public string Value { get; set; }
    }
}