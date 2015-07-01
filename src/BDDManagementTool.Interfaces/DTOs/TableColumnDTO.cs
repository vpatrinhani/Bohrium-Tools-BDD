using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableColumnDTO : BaseDTO, IIdentifiable
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
    }
}