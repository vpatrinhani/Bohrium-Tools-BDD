using AutoMapper;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using Bohrium.Tools.BDDManagementTool.Interfaces;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using Microsoft.Practices.Unity;

namespace Bohrium.Tools.BDDManagementTool.Services
{
    public class ServicesBootstrap
    {
        private readonly IUnityContainer container;

        public ServicesBootstrap(IUnityContainer container)
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
            this.container.RegisterType<ISearchService, SearchService>();
        }

        private void configureAutoMapper()
        {
            Mapper.CreateMap<FilterRepoParamDTO, FilterRepoParam>();

            Mapper.CreateMap<FeatureVO, FeatureDTO>();
            Mapper.CreateMap<ScenarioVO, ScenarioDTO>();
            Mapper.CreateMap<StatementVO, StatementDTO>();
            Mapper.CreateMap<StepDefinitionVO, StepDefinitionDTO>();
            Mapper.CreateMap<StepDefinitionTypeVO, StepDefinitionTypeDTO>();
            Mapper.CreateMap<TableCellVO, TableCellDTO>();
            Mapper.CreateMap<TableColumnVO, TableColumnDTO>();
            Mapper.CreateMap<TableParameterVO, TableParameterDTO>();
            Mapper.CreateMap<TableRowVO, TableRowDTO>();

            Mapper.CreateMap<BaseVO, BaseDTO>()
                .Include<FeatureVO, FeatureDTO>()
                .Include<ScenarioVO, ScenarioDTO>()
                .Include<StatementVO, StatementDTO>()
                .Include<StepDefinitionVO, StepDefinitionDTO>()
                .Include<StepDefinitionTypeVO, StepDefinitionTypeDTO>()
                .Include<TableCellVO, TableCellDTO>()
                .Include<TableColumnVO, TableColumnDTO>()
                .Include<TableParameterVO, TableParameterDTO>()
                .Include<TableRowVO, TableRowDTO>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}