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
using Bohrium.Tools.BDDManagementTool.WebApp.Utils;
using Bohrium.Tools.BDDManagementTool.Data.XML;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data.XML.Repository;
using Bohrium.Tools.BDDManagementTool.Presentation;

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
            //configureXMLStorage();
            configureEntityFrameworkStorage();

            configureServices();

            configurePresentation();
        }

        #endregion Protected

        #region Private

        private void configureServices()
        {
            Container.RegisterType<ServicesBootstrap, ServicesBootstrap>(new ContainerControlledLifetimeManager());

            Container.Resolve<ServicesBootstrap>().Configure();
        }

        private void configurePresentation()
        {
            Container.RegisterType<PresentationBootstrap, PresentationBootstrap>(new ContainerControlledLifetimeManager());

            Container.Resolve<PresentationBootstrap>().Configure();
        }

        private void configureXMLStorage()
        {
            Container.RegisterInstance<
                Bohrium.Tools.BDDManagementTool.Data.XML.XmlDataContext>(
                    new Bohrium.Tools.BDDManagementTool.Data.XML.XmlDataContext(HttpContext.Current.Server.MapPath("~/App_Data/")),
                    new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());

            Container.RegisterType<
                Bohrium.Tools.BDDManagementTool.Data.XML.DataBootstrap, 
                Bohrium.Tools.BDDManagementTool.Data.XML.DataBootstrap>(new ContainerControlledLifetimeManager());

            Container.Resolve<Bohrium.Tools.BDDManagementTool.Data.XML.DataBootstrap>().Configure();
        }

        private void configureEntityFrameworkStorage()
        {
            Container.RegisterType<
                Bohrium.Tools.BDDManagementTool.Data.EntityFramework.BDDMgmtDbContext,
                Bohrium.Tools.BDDManagementTool.Data.EntityFramework.BDDMgmtDbContext>(new Microsoft.Practices.Unity.PerRequestLifetimeManager());

            Container.RegisterType<
                Bohrium.Tools.BDDManagementTool.Data.EntityFramework.DataBootstrap, 
                Bohrium.Tools.BDDManagementTool.Data.EntityFramework.DataBootstrap>(new ContainerControlledLifetimeManager());

            Container.Resolve<Bohrium.Tools.BDDManagementTool.Data.EntityFramework.DataBootstrap>().Configure();
        }

        #endregion Private

        #endregion Methods
    }
}