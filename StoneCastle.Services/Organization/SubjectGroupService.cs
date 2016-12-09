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
    public class SubjectGroupService : BaseService, ISubjectGroupService
    {
        public SubjectGroupService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<SubjectGroupModel> SearchSubjectGroup(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<SubjectGroupModel> response = new SearchResponse<SubjectGroupModel>();

            List<Models.SubjectGroup> subjectGroups = this.UnitOfWork.SubjectGroupRepository.SearchSubjectGroup(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var subjectGroupsModels = Mapper.Map<List<Models.SubjectGroup>, List<Models.SubjectGroupModel>>(subjectGroups);
            response.Records = subjectGroupsModels;
            response.Total = subjectGroupsModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<SubjectGroupModel> SearchSemesterSubjectGroup(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<SubjectGroupModel> response = new SearchResponse<SubjectGroupModel>();

            List<Models.SubjectGroup> subjectGroups = this.UnitOfWork.SubjectGroupRepository.SearchSemesterSubjectGroup(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var subjectGroupsModels = Mapper.Map<List<Models.SubjectGroup>, List<Models.SubjectGroupModel>>(subjectGroups);
            response.Records = subjectGroupsModels;
            response.Total = subjectGroupsModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<SubjectGroupModel> GetSemesterSubjectGroup(Guid semesterId)
        {
            Logger.Debug($"GetSemesterSubjectGroup: {semesterId}");

            SearchResponse<SubjectGroupModel> response = new SearchResponse<SubjectGroupModel>();

            List<Models.SubjectGroup> subjectGroups = this.UnitOfWork.SubjectGroupRepository.GetSemesterSubjectGroup(semesterId);

            var subjectGroupsModels = Mapper.Map<List<Models.SubjectGroup>, List<Models.SubjectGroupModel>>(subjectGroups);
            response.Records = subjectGroupsModels;
            response.Total = subjectGroupsModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }

        public List<SubjectGroupModel> GetSemesterSubjectByGroup(Guid semesterId)
        {
            Logger.Debug($"GetSemesterSubjectByGroup: {semesterId}");

            SearchResponse<SubjectGroupModel> response = new SearchResponse<SubjectGroupModel>();

            List<Models.SubjectGroup> subjectGroups = this.UnitOfWork.SubjectGroupRepository.GetSemesterSubjectGroup(semesterId);


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Organization.Models.SubjectGroup, Organization.Models.SubjectGroupModel>();
                cfg.CreateMap<Organization.Models.Subject, Organization.Models.SubjectModel>().ForMember(x=>x.SubjectGroup, y=>y.Ignore());
            });

            var mapper = config.CreateMapper();

            var subjectGroupsModels = mapper.Map<List<Models.SubjectGroup>, List<Models.SubjectGroupModel>>(subjectGroups);

            return subjectGroupsModels;
        }

        public SubjectGroupModel GetSubjectGroup(SubjectGroupModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            SubjectGroup subjectGroup = this.UnitOfWork.SubjectGroupRepository.GetById(model.Id);

            SubjectGroupModel subjectGroupModel = Mapper.Map<Models.SubjectGroup, Models.SubjectGroupModel>(subjectGroup);

            return subjectGroupModel; 
        }

        public SubjectGroupModel CreateOrUpdate(SubjectGroupModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            SubjectGroup subjectGroup = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                subjectGroup = this.UnitOfWork.SubjectGroupRepository.CreateSubjectGroup(model.SemesterId, model.Name, model.Code, model.HighlightColor, model.IsActive);
            }
            else
            {
                subjectGroup = this.UnitOfWork.SubjectGroupRepository.UpdateSubjectGroup(model.Id, model.Name, model.Code, model.HighlightColor, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            SubjectGroupModel subjectGroupModel = Mapper.Map<Models.SubjectGroup, Models.SubjectGroupModel>(subjectGroup);

            return subjectGroupModel;
        }
    }
}
