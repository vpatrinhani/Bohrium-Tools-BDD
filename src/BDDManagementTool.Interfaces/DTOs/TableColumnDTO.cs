using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;

namespace Bohrium.Tools.BDDManagementTool.Interfaces.DTOs
{
    public class TableColumnDTO : BaseDTO, IIdentifiable
    {
        public Guid ObjectId { get; set; }
        public string Value { get; set; }
    }
}
