using AutoMapper;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bohrium.Tools.BDDManagementTool.Data;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using Bohrium.Tools.BDDManagementTool.Services;
using Bohrium.Tools.BDDManagementTool.WebApp.App_Start;
using Bohrium.Tools.BDDManagementTool.WebApp.ViewModels;
using Bohrium.Tools.BDDManagementTool.WebApp.Utils;
using Bohrium.Tools.BDDManagementTool.Data.XML;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data.XML.Repository;

namespace Bohrium.Tools.BDDManagementTool.WebApp
{
    public class WebAppContext
    {
        #region Singleton Implementation

        private static volatile WebAppContext current;
        private static object syncRoot = new Object();

        private WebAppContext() {

            Container = new UnityContainer();
        }

        public static WebAppContext Current
        {
            get
            {
                if (current == null)
                {
                    lock (syncRoot)
                    {
                        if (current == null)
                            current = new WebAppContext();
                    }
                }

                return current;
            }
        }

        #endregion Singleton Implementation

        #region Fields

        private static volatile bool _initializing;
        private static volatile bool _initialized;

        #endregion Fields

        #region Properties

        public bool IsInitializing
        {
            get { return _initializing; }
            protected set { _initializing = value; }
        }

        public bool IsInitialized
        {
            get { return _initialized; }
            protected set { _initialized = value; }
        }

        public IUnityContainer Container { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Method responsible to read everything that was set calling the Configure method and initialize them.
        /// </summary>
        public virtual void Initialize()
        {
            lock (syncRoot)
            {
                if (IsInitializing) return;
            }

            StartingInitialization();

            lock (syncRoot)
            {
                if (IsInitialized) return;
            }

            Configure();

            FinishingInitialization();
        }

        #region Protected

        /// <summary>
        /// This method is called into the beginning of Initialize method before the Configure call.
        /// </summary>
        protected virtual void StartingInitialization()
        {
            lock (syncRoot)
            {
                IsInitializing = true;
            }
        }

        /// <summary>
        /// This method is called into the end of Initialize method after the Configure call.
        /// </summary>
        protected virtual void FinishingInitialization()
        {
            lock (syncRoot)
            {
                IsInitializing = false;
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Method responsible to configure the AppContext
        /// </summary>
        protected virtual void Configure()
        {
            Container.RegisterInstance<IDataContext>(new XmlDataContext(HttpContext.Current.Server.MapPath("~/App_Data/")), new ContainerControlledLifetimeManager());

            Container.RegisterType<ISearchRepository, SearchRepository>();
            Container.RegisterType<ServicesBootstrap, ServicesBootstrap>(new ContainerControlledLifetimeManager());

            Container.Resolve<ServicesBootstrap>().Configure();

            initializeAutoMapper();
        }

        #endregion Protected

        #region Private

        private void initializeAutoMapper()
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
                        RegexStatement = x.RegexStatement,
                        Type = x.Type
                    }).ToList());

            Mapper.CreateMap<TableColumnDTO, TableColumnViewModel>();

            Mapper.CreateMap<TableRowDTO, TableRowViewModel>();

            Mapper.CreateMap<TableCellDTO, TableCellViewModel>();

            Mapper.AssertConfigurationIsValid();
        }

        #endregion Private

        #endregion Methods
    }
}