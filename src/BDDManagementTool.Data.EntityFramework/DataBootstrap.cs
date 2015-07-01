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
            Mapper.CreateMap<Step, StatementVO>();
            Mapper.CreateMap<StepDefinition, StepDefinitionVO>();
            Mapper.CreateMap<StepDefinitionType, StepDefinitionTypeVO>();
            Mapper.CreateMap<TableParameterCell, TableCellVO>();
            Mapper.CreateMap<TableParameterColumn, TableColumnVO>();
            Mapper.CreateMap<TableParameter, TableParameterVO>();
            Mapper.CreateMap<TableParameterRow, TableRowVO>();

            Mapper.CreateMap<BaseEntity, BaseVO>()
                .Include<Feature, FeatureVO>()
                .Include<Scenario, ScenarioVO>()
                .Include<Step, StatementVO>()
                .Include<StepDefinition, StepDefinitionVO>()
                .Include<StepDefinitionType, StepDefinitionTypeVO>()
                .Include<TableParameterCell, TableCellVO>()
                .Include<TableParameterColumn, TableColumnVO>()
                .Include<TableParameter, TableParameterVO>()
                .Include<TableParameterRow, TableRowVO>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}