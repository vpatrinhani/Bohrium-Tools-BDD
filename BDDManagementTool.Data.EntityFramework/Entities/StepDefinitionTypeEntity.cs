﻿using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities
{
    public class StepDefinitionTypeEntity : BaseEntity, IStepDefinitionTypeEntity
    {
        public Guid ObjectId { get; set; }
        public string Type { get; set; }
        public Guid StepDefinitionId { get; set; }
        public IStepDefinitionEntity StepDefinition { get; set; }
        public string RegexStatement { get; set; }
        public int CountUsages { get; set; }
    }
}