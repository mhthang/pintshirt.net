using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;
using System;
using System.Linq;

namespace StoneCastle.Organization.Services
{
    public class ClassGroupService : BaseService, IClassGroupService
    {
        public ClassGroupService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<ClassGroupModel> SearchClassGroup(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<ClassGroupModel> response = new SearchResponse<ClassGroupModel>();

            List<Models.ClassGroup> groups = this.UnitOfWork.ClassGroupRepository.SearchSemesterClassGroup(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);
            long total = this.UnitOfWork.ClassGroupRepository.CountSemesterClassGroup(request.Id, request.FilterText);

            var classGroupsModels = Mapper.Map<List<Models.ClassGroup>, List<Models.ClassGroupModel>>(groups);
            response.Records = classGroupsModels;
            response.Total = total;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<ClassGroupModel> GetSemesterClassGroup(Guid semesterId)
        {
            Logger.Debug($"GetSemesterClassGroup: {semesterId}");

            if (semesterId == null || semesterId == Guid.Empty)
                throw new System.ArgumentNullException("semesterId");

            SearchResponse<ClassGroupModel> response = new SearchResponse<ClassGroupModel>();

            IEnumerable<Models.ClassGroup> groups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(semesterId);

            var classGroupsModels = Mapper.Map<List<Models.ClassGroup>, List<Models.ClassGroupModel>>(groups.ToList());
            response.Records = classGroupsModels;
            response.Total = classGroupsModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }

        public ClassGroupModel GetClassGroup(ClassGroupModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            ClassGroup group = this.UnitOfWork.ClassGroupRepository.GetById(model.Id);

            ClassGroupModel groupModel = Mapper.Map<Models.ClassGroup, Models.ClassGroupModel>(group);

            return groupModel; 
        }

        public ClassGroupModel CreateOrUpdate(ClassGroupModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            ClassGroup group = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                TrainingProgram.Models.TrainingProgram program = this.UnitOfWork.TrainingProgramRepository.CreateTrainingProgram(model.SemesterId, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);

                group = this.UnitOfWork.ClassGroupRepository.CreateClassGroup(model.SemesterId, program.Id, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                group = this.UnitOfWork.ClassGroupRepository.UpdateClassGroup(model.Id, model.SemesterId, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            ClassGroupModel groupModel = Mapper.Map<Models.ClassGroup, Models.ClassGroupModel>(group);

            return groupModel;
        }
    }
}
