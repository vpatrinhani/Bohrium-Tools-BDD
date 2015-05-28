using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bohrium.Tools.BDDManagementTool.WebApp.Utils;
using Bohrium.Tools.BDDManagementTool.WebApp.ViewModels;

namespace BDDManagementTool.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnityContainer container;
        
        public SearchController(IUnityContainer container)
        {
            this.container = container;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result(SearchFilterType type, string text)
        {
            ViewBag.searchType = (int)type;
            ViewBag.searchText = text;

            var searchService = container.Resolve<ISearchService>();
             
            var result = searchService.Search(type, text).To<IEnumerable<BaseSearchResultViewModel>>();

            return View("Index", result);
        }

        [Route("feature/{id}")]
        public ActionResult Feature(Guid id)
        {
            var searchService = container.Resolve<ISearchService>();

            var result = searchService.GetFeatureById(id);

            if (result == null)
                return HttpNotFound();

            var model = result.To<FeatureSearchResultViewModel>();

            return View(model);
        }

        [Route("step-definition/{id}")]
        public ActionResult StepDefinition(Guid id)
        {
            var searchService = container.Resolve<ISearchService>();

            var result = searchService.GetStepDefinitionById(id);

            if (result == null)
                return HttpNotFound();

            var model = result.To<StepDefinitionSearchResultViewModel>();

            return View(model);
        }

        [Route("scenario/{id}")]
        public ActionResult Scenario(Guid id)
        {
            var searchService = container.Resolve<ISearchService>();

            var result = searchService.GetScenarioById(id);

            if (result == null)
                return HttpNotFound();

            var model = result.To<ScenarioSearchResultViewModel>();

            return View(model);
        }
	}
}