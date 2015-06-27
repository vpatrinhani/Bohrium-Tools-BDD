using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Microsoft.Practices.Unity;
using Bohrium.Tools.BDDManagementTool.Interfaces;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data;

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

            Mapper.CreateMap<IFeatureEntity, FeatureDTO>();
            Mapper.CreateMap<IScenarioEntity, ScenarioDTO>();
            Mapper.CreateMap<IStatementEntity, StatementDTO>();
            Mapper.CreateMap<IStepDefinitionEntity, StepDefinitionDTO>();
            Mapper.CreateMap<IStepDefinitionTypeEntity, StepDefinitionTypeDTO>();
            Mapper.CreateMap<ITableCellEntity, TableCellDTO>();
            Mapper.CreateMap<ITableColumnEntity, TableColumnDTO>();
            Mapper.CreateMap<ITableParameterEntity, TableParameterDTO>();
            Mapper.CreateMap<ITableRowEntity, TableRowDTO>();

            Mapper.CreateMap<IBaseEntity, BaseDTO>()
                .Include<IFeatureEntity, FeatureDTO>()
                .Include<IScenarioEntity, ScenarioDTO>()
                .Include<IStatementEntity, StatementDTO>()
                .Include<IStepDefinitionEntity, StepDefinitionDTO>()
                .Include<IStepDefinitionTypeEntity, StepDefinitionTypeDTO>()
                .Include<ITableCellEntity, TableCellDTO>()
                .Include<ITableColumnEntity, TableColumnDTO>()
                .Include<ITableParameterEntity, TableParameterDTO>()
                .Include<ITableRowEntity, TableRowDTO>();
        }
    }
}
