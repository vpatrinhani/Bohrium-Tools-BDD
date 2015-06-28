using AutoMapper;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Microsoft.Practices.Unity;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework
{
    public class DataBootstrap
    {
        private readonly IUnityContainer container;

        public DataBootstrap(IUnityContainer container)
        {
            this.container = container;
        }

        public void Configure()
        {
            configureAutoMapper();

            configureContainer();
        }

        private void configureContainer()
        {
            this.container.RegisterType<IUnitOfWork, UnitOfWork>();

            this.container.RegisterType<ISearchRepository, SearchRepository>();
        }

        private void configureAutoMapper()
        {
            Mapper.CreateMap<Feature, FeatureVO>();
            Mapper.CreateMap<Scenario, ScenarioVO>();
            Mapper.CreateMap<Statement, StatementVO>();
            Mapper.CreateMap<StepDefinition, StepDefinitionVO>();
            Mapper.CreateMap<StepDefinitionType, StepDefinitionTypeVO>();
            Mapper.CreateMap<TableCell, TableCellVO>();
            Mapper.CreateMap<TableColumn, TableColumnVO>();
            Mapper.CreateMap<TableParameter, TableParameterVO>();
            Mapper.CreateMap<TableRow, TableRowVO>();

            Mapper.CreateMap<BaseEntity, BaseVO>()
                .Include<Feature, FeatureVO>()
                .Include<Scenario, ScenarioVO>()
                .Include<Statement, StatementVO>()
                .Include<StepDefinition, StepDefinitionVO>()
                .Include<StepDefinitionType, StepDefinitionTypeVO>()
                .Include<TableCell, TableCellVO>()
                .Include<TableColumn, TableColumnVO>()
                .Include<TableParameter, TableParameterVO>()
                .Include<TableRow, TableRowVO>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}