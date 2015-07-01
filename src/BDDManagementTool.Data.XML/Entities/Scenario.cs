using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Entities
{
    public class Scenario : BaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public Guid FeatureObjectId { get; set; }
        public Feature Feature { get; set; }
        public IList<Statement> Statements { get; set; }

        public Scenario()
        {
            Statements = new List<Statement>();
        }
    }
}