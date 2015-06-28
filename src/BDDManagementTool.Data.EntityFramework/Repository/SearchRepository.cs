using AutoMapper;
using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository
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

        public virtual IEnumerable<BaseVO> Search(FilterRepoParam[] filters)
        {
            List<BaseVO> returnedItems = new List<BaseVO>();
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

        public FeatureVO GetFeatureById(Guid id)
        {
            var queryResult = UnitOfWork.Context.Features.FirstOrDefault(x => x.ObjectId == id);

            return Mapper.Map<FeatureVO>(queryResult);
        }

        public ScenarioVO GetScenarioById(Guid id)
        {
            var queryResult = UnitOfWork.Context.Scenarios.FirstOrDefault(x => x.ObjectId == id);

            return Mapper.Map<ScenarioVO>(queryResult);
        }

        public StepDefinitionVO GetStepDefinitionById(Guid id)
        {
            var queryResult = UnitOfWork.Context.StepDefinitions.FirstOrDefault(x => x.ObjectId == id);

            return Mapper.Map<StepDefinitionVO>(queryResult);
        }

        private IEnumerable<BaseVO> SearchByStepDefinition(string filterText)
        {
            var queryResult = UnitOfWork.Context.StepDefinitions.Where(i => i.MethodName.ToLower().Contains(filterText)).ToList();

            return Mapper.Map<IEnumerable<BaseVO>>(queryResult);
        }

        private IEnumerable<BaseVO> SearchByScenario(string filterText)
        {
            var queryResult = UnitOfWork.Context.Scenarios.Where(i => i.Description.ToLower().Contains(filterText)).ToList();

            return Mapper.Map<IEnumerable<BaseVO>>(queryResult);
        }

        private IEnumerable<FeatureVO> SearchByFeature(string filterText)
        {
            var queryResult = UnitOfWork.Context.Features.Where(i => i.Description.ToLower().Contains(filterText)).ToList();

            return Mapper.Map<IEnumerable<FeatureVO>>(queryResult);
        }

        private IEnumerable<StatementVO> SearchByGiven(string filterText)
        {
            var queryResult = UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "given" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();

            return Mapper.Map<IEnumerable<StatementVO>>(queryResult);
        }

        private IEnumerable<StatementVO> SearchByWhen(string filterText)
        {
            var queryResult = UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "when" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();

            return Mapper.Map<IEnumerable<StatementVO>>(queryResult);
        }

        private IEnumerable<StatementVO> SearchByThen(string filterText)
        {
            var queryResult = UnitOfWork.Context.Statements.Where(i => i.Type.ToLower() == "then" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Any(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();

            return Mapper.Map<IEnumerable<StatementVO>>(queryResult);
        }

        private IEnumerable<BaseVO> SearchByTag(string filterText)
        {
            List<BaseEntity> returnedItems = new List<BaseEntity>();

            //Load Features
            returnedItems.AddRange(UnitOfWork.Context.Features.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));

            //Load Scenarios
            returnedItems.AddRange(UnitOfWork.Context.Scenarios.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));

            return Mapper.Map<IEnumerable<BaseVO>>(returnedItems);
        }

        private IEnumerable<BaseVO> SearchByAll(string filterText)
        {
            List<BaseVO> returnedItems = new List<BaseVO>();

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