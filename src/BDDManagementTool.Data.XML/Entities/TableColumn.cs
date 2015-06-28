using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class TableColumn : BaseEntity
    {
        public Guid ObjectId { get; set; }
        public string Value { get; set; }
    }
}