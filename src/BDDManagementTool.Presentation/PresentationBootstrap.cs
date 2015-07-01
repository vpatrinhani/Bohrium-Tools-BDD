using AutoMapper;
using Bohrium.Tools.BDDManagementTool.Infrastructure.Core.Extensions;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using Bohrium.Tools.BDDManagementTool.Presentation.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Bohrium.Tools.BDDManagementTool.Presentation
{
    public class PresentationBootstrap
    {
        private readonly IUnityContainer container;

        public PresentationBootstrap(IUnityContainer container)
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
        }

        private void configureAutoMapper()
        {
            Mapper.CreateMap<BaseDTO, BaseSearchResultViewModel>()
                .Ignore(viewModel => viewModel.Id)
                .Include<FeatureDTO, FeatureSearchResultViewModel>()
                .Include<ScenarioDTO, ScenarioSearchResultViewModel>()
                .Include<StatementDTO, StatementSearchResultViewModel>()
                .Include<StepDefinitionDTO, StepDefinitionSearchResultViewModel>();

            Mapper.CreateMap<StatementDTO, StatementSearchResultViewModel>()
                .Ignore(viewModel => viewModel.Tables)
                .AfterMap((dto, viewModel) =>
                {
                    var tables = new List<TableViewModel>();

                    foreach (var tableParameterDto in dto.TableParameters)
                    {
                        var table = new TableViewModel();

                        table.Columns = Mapper.Map<TableColumnViewModel[]>(tableParameterDto.Columns);
                        table.Rows = Mapper.Map<TableRowViewModel[]>(tableParameterDto.Rows);

                        tables.Add(table);
                    }

                    viewModel.Tables = tables.ToArray();
                });

            Mapper.CreateMap<FeatureDTO, FeatureSearchResultViewModel>();

            Mapper.CreateMap<ScenarioDTO, ScenarioSearchResultViewModel>();

            Mapper.CreateMap<StepDefinitionDTO, StepDefinitionSearchResultViewModel>()
                .MapFrom(viewModel => viewModel.StepDefinitionTypes,
                    dto => dto.StepDefinitionTypes.Select(x => new StepDefinitionTypeViewModel()
                    {
                        Id = x.Id,
                        CountUsages = x.CountUsages,
                        RegexExpression = x.RegexExpression,
                        Type = x.Type
                    }).ToList());

            Mapper.CreateMap<TableColumnDTO, TableColumnViewModel>();

            Mapper.CreateMap<TableRowDTO, TableRowViewModel>();

            Mapper.CreateMap<TableCellDTO, TableCellViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}