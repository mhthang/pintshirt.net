using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;
using System;

namespace StoneCastle.Organization.Services
{
    public class BuildingService : BaseService, IBuildingService
    {
        public BuildingService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<BuildingModel> SearchBuilding(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<BuildingModel> response = new SearchResponse<BuildingModel>();

            List<Models.Building> centers = this.UnitOfWork.BuildingRepository.SearchBuilding(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var centerModels = Mapper.Map<List<Models.Building>, List<Models.BuildingModel>>(centers);
            response.Records = centerModels;
            response.Total = centerModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<BuildingModel> SearchSemesterBuilding(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<BuildingModel> response = new SearchResponse<BuildingModel>();

            List<Models.Building> centers = this.UnitOfWork.BuildingRepository.SearchSemesterBuilding(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var centerModels = Mapper.Map<List<Models.Building>, List<Models.BuildingModel>>(centers);
            response.Records = centerModels;
            response.Total = centerModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<BuildingModel> GetSemesterBuilding(Guid semesterId)
        {
            Logger.Debug($"Search: {semesterId}");

            SearchResponse<BuildingModel> response = new SearchResponse<BuildingModel>();

            List<Models.Building> centers = this.UnitOfWork.BuildingRepository.GetSemesterBuilding(semesterId);

            var centerModels = Mapper.Map<List<Models.Building>, List<Models.BuildingModel>>(centers);
            response.Records = centerModels;
            response.Total = centerModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }

        public BuildingModel GetBuilding(BuildingModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Building center = this.UnitOfWork.BuildingRepository.GetById(model.Id);

            BuildingModel centerModel = Mapper.Map<Models.Building, Models.BuildingModel>(center);

            return centerModel; 
        }

        public BuildingModel CreateOrUpdate(BuildingModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Building center = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                center = this.UnitOfWork.BuildingRepository.CreateBuilding(model.SemesterId, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                center = this.UnitOfWork.BuildingRepository.UpdateBuidling(model.Id, model.SemesterId, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            BuildingModel centerModel = Mapper.Map<Models.Building, Models.BuildingModel>(center);

            return centerModel;
        }
    }
}
