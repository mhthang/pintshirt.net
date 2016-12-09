using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;

namespace StoneCastle.Organization.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<OrganizationModel> SearchOrganization(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            SearchResponse<OrganizationModel> response = new SearchResponse<OrganizationModel>();

            List<Models.Organization> org = this.UnitOfWork.OrganizationRepository.SearchOrganization(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var orgModels = Mapper.Map<List<Models.Organization>, List<Models.OrganizationModel>>(org);
            response.Records = orgModels;
            response.Total = orgModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<UserOrganizationModel> GetUserOrganizations(string userId)
        {
            Logger.Debug($"Get User's Organizations: {userId}");

            SearchResponse<UserOrganizationModel> response = new SearchResponse<UserOrganizationModel>();

            List<Models.Organization> orgs = this.UnitOfWork.OrganizationRepository.GetUserOrganization(userId);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Organization.Models.Organization, Organization.Models.UserOrganizationModel>();
                cfg.CreateMap<Organization.Models.Semester, Organization.Models.SemesterModel>().ForMember(x=>x.Organization, y=>y.Ignore());
            });

            var mapper = config.CreateMapper();

            var orgModels = mapper.Map<List<Models.Organization>, List<Models.UserOrganizationModel>>(orgs);
            response.Records = orgModels;
            response.Total = orgModels.Count;
            response.Pager = null;

            return response;
        }

        public OrganizationModel GetOrganization(OrganizationModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Organization.Models.Organization org = this.UnitOfWork.OrganizationRepository.GetById(model.Id);

            OrganizationModel orgModel = Mapper.Map<Models.Organization, Models.OrganizationModel>(org);

            return orgModel; 
        }

        public OrganizationModel CreateOrUpdate(OrganizationModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Organization.Models.Organization org = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                org = this.UnitOfWork.OrganizationRepository.CreateOrganization(model.Name, model.ShortName, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                org = this.UnitOfWork.OrganizationRepository.UpdateOrganization(model.Id, model.Name, model.ShortName, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            OrganizationModel orgModel = Mapper.Map<Models.Organization, Models.OrganizationModel>(org);

            return orgModel;
        }
    }
}
