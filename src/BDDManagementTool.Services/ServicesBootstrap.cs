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
            this.container.RegisterType<ISearchRepository, SearchRepository>();
            this.container.RegisterType<ISearchService, SearchService>();
        }

        private void configureAutoMapper()
        {
            Mapper.CreateMap<FilterRepoParamDTO, FilterRepoParam>();

            Mapper.CreateMap<FeatureEntity, FeatureDTO>();
            Mapper.CreateMap<ScenarioEntity, ScenarioDTO>();
            Mapper.CreateMap<StatementEntity, StatementDTO>();
            Mapper.CreateMap<StepDefinitionEntity, StepDefinitionDTO>();
            Mapper.CreateMap<StepDefinitionTypeEntity, StepDefinitionTypeDTO>();
            Mapper.CreateMap<TableCellEntity, TableCellDTO>();
            Mapper.CreateMap<TableColumnEntity, TableColumnDTO>();
            Mapper.CreateMap<TableParameterEntity, TableParameterDTO>();
            Mapper.CreateMap<TableRowEntity, TableRowDTO>();

            Mapper.CreateMap<BaseEntity, BaseDTO>()
                .Include<FeatureEntity, FeatureDTO>()
                .Include<ScenarioEntity, ScenarioDTO>()
                .Include<StatementEntity, StatementDTO>()
                .Include<StepDefinitionEntity, StepDefinitionDTO>()
                .Include<StepDefinitionTypeEntity, StepDefinitionTypeDTO>()
                .Include<TableCellEntity, TableCellDTO>()
                .Include<TableColumnEntity, TableColumnDTO>()
                .Include<TableParameterEntity, TableParameterDTO>()
                .Include<TableRowEntity, TableRowDTO>();
        }
    }
}
