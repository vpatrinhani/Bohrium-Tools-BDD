using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class StepDefinitionType : BaseEntity
    {
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public string RegexExpression { get; set; }
        [NotMapped]
        public int CountUsages { get; set; }
        [NotMapped]
        public virtual StepDefinition StepDefinition { get; set; }
    }
}