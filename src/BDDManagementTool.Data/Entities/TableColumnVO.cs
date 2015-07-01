using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class TableColumnVO : BaseVO, IIdentifiable
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
    }
}