using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableColumn : BaseEntity
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
    }
}