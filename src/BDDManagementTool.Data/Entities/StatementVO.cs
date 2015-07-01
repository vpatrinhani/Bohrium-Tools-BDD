using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StatementVO : BaseVO, IIdentifiable, ITypeable, IDescriptable
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public Guid ScenarioId { get; set; }
        public ScenarioVO Scenario { get; set; }
        public Guid FeatureId { get; set; }
        public FeatureVO Feature { get; set; }
        public Guid StepDefinitionId { get; set; }
        public StepDefinitionVO StepDefinition { get; set; }
        public IList<StepDefinitionTypeVO> StepDefinitionTypes { get; set; }
        public string MultilineTextParameter { get; set; }
        public IList<TableParameterVO> TableParameters { get; set; }
    }
}