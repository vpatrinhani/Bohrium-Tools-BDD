using AutoMapper;
using Bohrium.Tools.BDDManagementTool.Constraints.Enumerations;
using Bohrium.Tools.BDDManagementTool.Data;
using Bohrium.Tools.BDDManagementTool.Data.Repository;
using Bohrium.Tools.BDDManagementTool.Data.Repository.Params;
using Bohrium.Tools.BDDManagementTool.Interfaces;
using Bohrium.Tools.BDDManagementTool.Interfaces.DTOs;
using System;
using System.Collections.Generic;

namespace Bohrium.Tools.BDDManagementTool.Services
{
    public class SearchService : ISearchService
    {
        protected ISearchRepository SearchRepository { get; set; }

        public SearchService(ISearchRepository searchRepository)
        {
            SearchRepository = searchRepository;
        }

        public IEnumerable<BaseDTO> Search(string text)
        {
            return Search(SearchFilterType.All, text);
        }

        public IEnumerable<BaseDTO> Search(SearchFilterType filterType, string text)
        {
            var filters = new[]
            {
                new FilterRepoParamDTO()
                {
                    Type = filterType,
                    Text = text
                }
            };

            return Mapper.Map<IEnumerable<BaseDTO>>(SearchRepository.Search(Mapper.Map<FilterRepoParam[]>(filters)));
        }

        public FeatureDTO GetFeatureById(Guid id)
        {
            return Mapper.Map<FeatureDTO>(SearchRepository.GetFeatureById(id));
        }

        public ScenarioDTO GetScenarioById(Guid id)
        {
            return Mapper.Map<ScenarioDTO>(SearchRepository.GetScenarioById(id));
        }

        public StepDefinitionDTO GetStepDefinitionById(Guid id)
        {
            return Mapper.Map<StepDefinitionDTO>(SearchRepository.GetStepDefinitionById(id));
        }
    }
}
