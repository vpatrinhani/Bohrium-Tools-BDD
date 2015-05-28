using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bohrium.Tools.BDDManagementTool.Data.Entities;

namespace Bohrium.Tools.BDDManagementTool.Data
{
    /// <summary>
    /// Abstractions for Data Context to provide data for querying.
    /// </summary>
    public interface IDataContext
    {
        IQueryable<FeatureEntity> Features { get; }

        IQueryable<ScenarioEntity> Scenarios { get; }

        IQueryable<StatementEntity> Statements { get; }

        IQueryable<StepDefinitionEntity> StepDefinitions { get; }

        IQueryable<StepDefinitionTypeEntity> StepDefinitionTypes { get; }

        IQueryable<TableParameterEntity> Tables { get; }

        IQueryable<TableColumnEntity> TableColumns { get; }

        IQueryable<TableRowEntity> TableRows { get; }

        IQueryable<TableCellEntity> TableCells { get; }
    }
}

