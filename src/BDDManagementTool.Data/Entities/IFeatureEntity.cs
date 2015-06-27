using System;
using System.Collections.Generic;
using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public interface IFeatureEntity : IBaseEntity, IIdentifiable, IDescriptable, ITaggable
    {
        Guid Id { get; set; }
        string Description { get; set; }
        string[] Tags { get; set; }
        IList<IScenarioEntity> Scenarios { get; set; }
        IList<IStatementEntity> Background { get; set; }
    }
}