using Bohrium.Tools.BDDManagementTool.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDDManagementTool.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnityContainer container;
        
        public HomeController(IUnityContainer container)
        {
            this.container = container;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Search");
        }
    }
}