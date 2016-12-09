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
    public class DivisionService : BaseService, IDivisionService
    {
        public DivisionService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<DivisionModel> SearchDivisions(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<DivisionModel> response = new SearchResponse<DivisionModel>();

            List<Models.Division> divisions = this.UnitOfWork.DivisionRepository.SearchSemesterDivision(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var divisionModels = Mapper.Map<List<Models.Division>, List<Models.DivisionModel>>(divisions);
            response.Records = divisionModels;
            response.Total = divisionModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<DivisionModel> GetSemesterDivision(Guid semesterId)
        {
            Logger.Debug($"GetSemesterDivision: {semesterId}");

            if (semesterId == null || semesterId == Guid.Empty)
                throw new System.ArgumentNullException("semesterId");

            SearchResponse<DivisionModel> response = new SearchResponse<DivisionModel>();

            List<Models.Division> divisions = this.UnitOfWork.DivisionRepository.GetSemesterDivision(semesterId);

            var divisionModels = Mapper.Map<List<Models.Division>, List<Models.DivisionModel>>(divisions);
            response.Records = divisionModels;
            response.Total = divisionModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }

        public DivisionModel GetDivision(DivisionModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Division division = this.UnitOfWork.DivisionRepository.GetById(model.Id);

            DivisionModel divisionModel = Mapper.Map<Models.Division, Models.DivisionModel>(division);

            return divisionModel; 
        }

        public DivisionModel CreateOrUpdate(DivisionModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Division division = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                division = this.UnitOfWork.DivisionRepository.CreateDivision(model.Name, model.SemesterId, model.LogoUrl, model.LogoUrl, model.IsActive);
            }
            else
            {
                division = this.UnitOfWork.DivisionRepository.UpdateDivision(model.Id, model.Name, model.LogoUrl, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            DivisionModel divisionModel = Mapper.Map<Models.Division, Models.DivisionModel>(division);

            return divisionModel;
        }
    }
}
