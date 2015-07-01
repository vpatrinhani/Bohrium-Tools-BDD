using System;

namespace Bohrium.Tools.BDDManagementTool.Constraints.Interfaces
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}