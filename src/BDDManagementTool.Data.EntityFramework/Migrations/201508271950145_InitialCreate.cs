namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        FeatureId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        StepDefinitionId = c.Guid(),
                        ScenarioId = c.Guid(),
                        FeatureId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId)
                .ForeignKey("dbo.Scenarios", t => t.ScenarioId)
                .ForeignKey("dbo.StepDefinitions", t => t.StepDefinitionId)
                .Index(t => t.StepDefinitionId)
                .Index(t => t.ScenarioId)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.StepDefinitions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MethodName = c.String(),
                        MethodSignature = c.String(),
                        CountUsages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StepDefinitionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        StepDefinitionId = c.Guid(nullable: false),
                        RegexExpression = c.String(),
                        Step_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StepDefinitions", t => t.StepDefinitionId, cascadeDelete: true)
                .ForeignKey("dbo.Steps", t => t.Step_Id)
                .Index(t => t.StepDefinitionId)
                .Index(t => t.Step_Id);
            
            CreateTable(
                "dbo.TableParameters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StepId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.TableParameterColumns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TableParameterId = c.Guid(nullable: false),
                        Label = c.String(),
                        CellType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableParameters", t => t.TableParameterId, cascadeDelete: true)
                .Index(t => t.TableParameterId);
            
            CreateTable(
                "dbo.TableParameterRows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TableParameterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableParameters", t => t.TableParameterId, cascadeDelete: false)
                .Index(t => t.TableParameterId);
            
            CreateTable(
                "dbo.TableParameterCells",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TableParameterColumnId = c.Guid(nullable: false),
                        TableParameterRowId = c.Guid(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableParameterColumns", t => t.TableParameterColumnId, cascadeDelete: true)
                .ForeignKey("dbo.TableParameterRows", t => t.TableParameterRowId, cascadeDelete: true)
                .Index(t => t.TableParameterColumnId)
                .Index(t => t.TableParameterRowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableParameters", "StepId", "dbo.Steps");
            DropForeignKey("dbo.TableParameterRows", "TableParameterId", "dbo.TableParameters");
            DropForeignKey("dbo.TableParameterCells", "TableParameterRowId", "dbo.TableParameterRows");
            DropForeignKey("dbo.TableParameterCells", "TableParameterColumnId", "dbo.TableParameterColumns");
            DropForeignKey("dbo.TableParameterColumns", "TableParameterId", "dbo.TableParameters");
            DropForeignKey("dbo.StepDefinitionTypes", "Step_Id", "dbo.Steps");
            DropForeignKey("dbo.Steps", "StepDefinitionId", "dbo.StepDefinitions");
            DropForeignKey("dbo.StepDefinitionTypes", "StepDefinitionId", "dbo.StepDefinitions");
            DropForeignKey("dbo.Steps", "ScenarioId", "dbo.Scenarios");
            DropForeignKey("dbo.Steps", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Scenarios", "FeatureId", "dbo.Features");
            DropIndex("dbo.TableParameterCells", new[] { "TableParameterRowId" });
            DropIndex("dbo.TableParameterCells", new[] { "TableParameterColumnId" });
            DropIndex("dbo.TableParameterRows", new[] { "TableParameterId" });
            DropIndex("dbo.TableParameterColumns", new[] { "TableParameterId" });
            DropIndex("dbo.TableParameters", new[] { "StepId" });
            DropIndex("dbo.StepDefinitionTypes", new[] { "Step_Id" });
            DropIndex("dbo.StepDefinitionTypes", new[] { "StepDefinitionId" });
            DropIndex("dbo.Steps", new[] { "FeatureId" });
            DropIndex("dbo.Steps", new[] { "ScenarioId" });
            DropIndex("dbo.Steps", new[] { "StepDefinitionId" });
            DropIndex("dbo.Scenarios", new[] { "FeatureId" });
            DropTable("dbo.TableParameterCells");
            DropTable("dbo.TableParameterRows");
            DropTable("dbo.TableParameterColumns");
            DropTable("dbo.TableParameters");
            DropTable("dbo.StepDefinitionTypes");
            DropTable("dbo.StepDefinitions");
            DropTable("dbo.Steps");
            DropTable("dbo.Scenarios");
            DropTable("dbo.Features");
        }
    }
}
