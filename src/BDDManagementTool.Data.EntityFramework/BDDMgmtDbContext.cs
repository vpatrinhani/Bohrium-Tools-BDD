namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
    using System.Data.Entity;

    public class BDDMgmtDbContext : DbContext
    {
        public BDDMgmtDbContext()
            : base("name=BDDMgmtDb")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<StepDefinition> StepDefinitions { get; set; }
        public DbSet<StepDefinitionType> StepDefinitionTypes { get; set; }
        public DbSet<TableParameter> Tables { get; set; }
        public DbSet<TableColumn> TableColumns { get; set; }
        public DbSet<TableRow> TableRows { get; set; }
        public DbSet<TableCell> TableCells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}