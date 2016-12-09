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
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<SubjectModel> SearchSubject(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<SubjectModel> response = new SearchResponse<SubjectModel>();

            List<Models.Subject> subjects = this.UnitOfWork.SubjectRepository.SearchSubject(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var subjectModels = Mapper.Map<List<Models.Subject>, List<Models.SubjectModel>>(subjects);
            response.Records = subjectModels;
            response.Total = subjectModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<SubjectModel> SearchSemesterSubject(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();


            SearchResponse<SubjectModel> response = new SearchResponse<SubjectModel>();
            
            List<Models.Subject> subjects = this.UnitOfWork.SubjectRepository.SearchSemesterSubject(request.Id, request.FilterId, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            long total = this.UnitOfWork.SubjectRepository.CountSemesterSubject(request.Id, request.FilterId, request.FilterText);

            var subjectModels = Mapper.Map<List<Models.Subject>, List<Models.SubjectModel>>(subjects);
            response.Records = subjectModels;
            response.Total = total;
            response.Pager = request.Pager;

            return response;
        }

        public SubjectModel GetSubject(SubjectModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Subject subject = this.UnitOfWork.SubjectRepository.GetById(model.Id);

            SubjectModel subjectModel = Mapper.Map<Models.Subject, Models.SubjectModel>(subject);

            return subjectModel; 
        }

        public SubjectModel CreateOrUpdate(SubjectModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Subject subject = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                subject = this.UnitOfWork.SubjectRepository.CreateSubject(model.Name, model.SubjectGroupId, model.HighlightColor, model.IsActive);
            }
            else
            {
                subject = this.UnitOfWork.SubjectRepository.UpdateSubject(model.Id, model.Name, model.SubjectGroupId, model.HighlightColor, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            SubjectModel subjectModel = Mapper.Map<Models.Subject, Models.SubjectModel>(subject);

            return subjectModel;
        }
    }
}
