using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using System.Collections.Generic;
using System;
using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Data.Entities;

namespace Bohrium.Tools.BDDManagementTool.Data.Repository
{
    public class SearchRepository : BaseRepository, ISearchRepository
    {
        public SearchRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public virtual IEnumerable<BaseEntity> Search(FilterRepoParam[] filters)
        {
            List<BaseEntity> returnedItems = new List<BaseEntity>();
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

        public FeatureEntity GetFeatureById(Guid id)
        {
            return this.DataContext.Features.FirstOrDefault(x => x.Id == id);
        }

        public ScenarioEntity GetScenarioById(Guid id)
        {
            return this.DataContext.Scenarios.FirstOrDefault(x => x.Id == id);
        }

        public StepDefinitionEntity GetStepDefinitionById(Guid id)
        {
            return this.DataContext.StepDefinitions.FirstOrDefault(x => x.Id == id);
        }

        private IEnumerable<BaseEntity> SearchByStepDefinition(string filterText)
        {
            return this.DataContext.StepDefinitions.Where(i => i.MethodName.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<BaseEntity> SearchByScenario(string filterText)
        {
            return this.DataContext.Scenarios.Where(i => i.Description.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<FeatureEntity> SearchByFeature(string filterText)
        {
            return this.DataContext.Features.Where(i => i.Description.ToLower().Contains(filterText)).ToList();
        }

        private IEnumerable<StatementEntity> SearchByGiven(string filterText)
        {
            return this.DataContext.Statements.Where(i => i.Type.ToLower() == "given" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Exists(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<StatementEntity> SearchByWhen(string filterText)
        {
            return this.DataContext.Statements.Where(i => i.Type.ToLower() == "when" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Exists(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<StatementEntity> SearchByThen(string filterText)
        {
            return this.DataContext.Statements.Where(i => i.Type.ToLower() == "then" && (i.Description.ToLower().Contains(filterText) || i.StepDefinitionTypes.Exists(j => j.RegexStatement.ToLower().Contains(filterText)))).ToList();
        }

        private IEnumerable<BaseEntity> SearchByTag(string filterText)
        {
            List<BaseEntity> returnedItems = new List<BaseEntity>();

            //Load Features
            returnedItems.AddRange(this.DataContext.Features.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));
            
            //Load Scenarios
            returnedItems.AddRange(this.DataContext.Scenarios.Where(i => i.Tags.ToList<string>().Exists(s => s.ToLower().Contains(filterText))));
            
            return returnedItems;
        }

        private IEnumerable<BaseEntity> SearchByAll(string filterText)
        {
            List<BaseEntity> returnedItems = new List<BaseEntity>();

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