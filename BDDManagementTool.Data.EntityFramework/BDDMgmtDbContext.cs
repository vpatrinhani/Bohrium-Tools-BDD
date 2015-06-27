namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    using Bohrium.Tools.BDDManagementTool.Data;
    using Bohrium.Tools.BDDManagementTool.Data.Entities;
    using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BDDMgmtDbContext : DbContext
    {
        public BDDMgmtDbContext()
            : base("name=BDDMgmtDb")
        {
        }

        public DbSet<FeatureEntity> Features { get; set; }
        public DbSet<ScenarioEntity> Scenarios { get; set; }
        public DbSet<StatementEntity> Statements { get; set; }
        public DbSet<StepDefinitionEntity> StepDefinitions { get; set; }
        public DbSet<StepDefinitionTypeEntity> StepDefinitionTypes { get; set; }
        public DbSet<TableParameterEntity> Tables { get; set; }
        public DbSet<TableColumnEntity> TableColumns { get; set; }
        public DbSet<TableRowEntity> TableRows { get; set; }
        public DbSet<TableCellEntity> TableCells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}