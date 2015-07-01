using Bohrium.Tools.BDDManagementTool.Constraints.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.Entities
{
    public class StepDefinitionVO : BaseVO, IIdentifiable
    {
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public string MethodSignature { get; set; }
        public int CountUsages { get; set; }
        public IList<StepDefinitionTypeVO> StepDefinitionTypes { get; set; }
    }
}