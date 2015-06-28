using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.Infrastructure
{
    public abstract class BaseSearchResultVO : IIdentifiable, IDescriptable
    {
        public virtual Guid ObjectId { get; set; }
        public virtual string Description { get; set; }
    }
}