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
        IQueryable<IFeatureEntity> Features { get; }

        IQueryable<IScenarioEntity> Scenarios { get; }

        IQueryable<IStatementEntity> Statements { get; }

        IQueryable<IStepDefinitionEntity> StepDefinitions { get; }

        IQueryable<IStepDefinitionTypeEntity> StepDefinitionTypes { get; }

        IQueryable<ITableParameterEntity> Tables { get; }

        IQueryable<ITableColumnEntity> TableColumns { get; }

        IQueryable<ITableRowEntity> TableRows { get; }

        IQueryable<ITableCellEntity> TableCells { get; }
    }
}

