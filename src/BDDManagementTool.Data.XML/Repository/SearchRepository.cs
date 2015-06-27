using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using Bohrium.Tools.BDDManagementTool.Data.XML.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bohrium.Tools.BDDManagementTool.Data.XML.Repository
{
    public class SearchRepository : ISearchRepository
    {
        protected UnitOfWork UnitOfWork { get; private set; }

        public SearchRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork as UnitOfWork;
            // TODO: Typed exception
            if (UnitOfWork == null) throw new Exception("Must be XmlUnitOfWork");
        }

        public virtual IEnumerable<IBaseEntity> Search(FilterRepoParam[] filters)
        {
            List<IBaseEntity> returnedItems = new List<IBaseEntity>();
            foreach (var filter in filters)
            {
                filter.Text = filter.Text.ToLower();
                switch (filter.Type)
                {
                    case SearchFilterType.Feature:
                        returnedItems.AddRange(SearchByFeature(filter.Text));
                        break;

                    case SearchFilterType.Scenario:
                        returnedItems.AddRange(SearchByScenario(filter.Text));
                        break;

                    case SearchFilterType.StepDefinition:
                        returnedItems.AddRange(SearchByStepDefinition(filter.Text));
                        break;

                    case SearchFilterType.Given:
                        returnedItems.AddRange(SearchByGiven(filter.Text));
                        break;

                    case SearchFilterType.When:
                        returnedItems.AddRange(SearchByWhen(filter.Text));
                        break;

                    case SearchFilterType.Then:
                        returnedItems.AddRange(SearchByThen(filter.Text));
                        break;

                    case SearchFilterType.Tag:
                        returnedItems.AddRange(SearchByTag(filter.Text));
                        break;

                    case SearchFilterType.All:
                        returnedItems.AddRange(SearchByAll(filter.Text));
                        break;
                }
            }

            return returnedItems;
        }

        public IFeatureEntity GetFeatureById(Guid id)
        {
            return UnitOfWork.Context.Features.FirstOrDefault(x => x.ObjectId == id);
        }

        public IScenarioEntity GetScenarioById(Guid id)
        {
            return UnitOfWork.Context.Scenarios.FirstOrDefault(x => x.ObjectId == id);
        }

        public IStepDefinitionEntity GetStepDefinitionById(Guid id)
        {
            return UnitOfWork.Context.StepDefinitions.FirstOrDefault(x => x.ObjectId == id);
        }

        private IEnumerable<IBaseEntity> SearchByStepDefinition(string filterText)
        {
            return UnitOfWork.Context.StepDefinitions.Where(i => i.MethodName.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<IBaseEntity> SearchByScenario(string filterText)
        {
            return UnitOfWork.Context.Scenarios.Where(i => i.Description.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<IFeatureEntity> SearchByFeature(string filterText)
        {
            return UnitOfWork.Context.Features.Where(i => i.Description.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<IStatementEntity> SearchByGiven(string filterText)
        {
            return UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "given" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<IStatementEntity> SearchByWhen(string filterText)
        {
            return UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "when" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<IStatementEntity> SearchByThen(string filterText)
        {
            return UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "then" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<IBaseEntity> SearchByTag(string filterText)
        {
            List<IBaseEntity> returnedItems = new List<IBaseEntity>();

            //Load Features
            returnedItems.AddRange(UnitOfWork.Context.Features.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));

            //Load Scenarios
            returnedItems.AddRange(UnitOfWork.Context.Scenarios.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));

            return returnedItems;
        }

        private IEnumerable<IBaseEntity> SearchByAll(string filterText)
        {
            List<IBaseEntity> returnedItems = new List<IBaseEntity>();

            returnedItems.AddRange(SearchByScenario(filterText));
            returnedItems.AddRange(SearchByFeature(filterText));
            returnedItems.AddRange(SearchByStepDefinition(filterText));
            returnedItems.AddRange(SearchByGiven(filterText));
            returnedItems.AddRange(SearchByWhen(filterText));
            returnedItems.AddRange(SearchByThen(filterText));
            returnedItems.AddRange(SearchByTag(filterText));

            return returnedItems;
        }
    }
}