namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class BDDMgmtDbContext : DbContext
    {
        public BDDMgmtDbContext()
            : base("name=BDDMgmtDb")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public IList<Feature> Features { get; set; }
        public IList<Scenario> Scenarios { get; set; }
        public IList<Step> Statements { get; set; }
        public DbSet<StepDefinition> StepDefinitions { get; set; }
        public DbSet<StepDefinitionType> StepDefinitionTypes { get; set; }
        public DbSet<TableParameter> Tables { get; set; }
        public DbSet<TableParameterColumn> TableColumns { get; set; }
        public DbSet<TableParameterRow> TableRows { get; set; }
        public DbSet<TableParameterCell> TableCells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}