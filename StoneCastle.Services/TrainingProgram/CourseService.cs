using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;
using StoneCastle.TrainingProgram.Models;

namespace StoneCastle.TrainingProgram.Services
{
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<CourseModel> SearchCourse(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");

            SearchResponse<CourseModel> response = new SearchResponse<CourseModel>();
            int total = 0;
            List<Models.Course> subjects = this.UnitOfWork.CourseRepository.SearchSemesterCourseSubject(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize, ref total);            

            var subjectModels = Mapper.Map<List<Models.Course>, List<Models.CourseModel>>(subjects);
            response.Records = subjectModels;
            response.Total = total;
            response.Pager = request.Pager;

            return response;
        }

        public CourseModel GetCourse(CourseModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Course subject = this.UnitOfWork.CourseRepository.GetById(model.Id);

            CourseModel subjectModel = Mapper.Map<Models.Course, Models.CourseModel>(subject);

            return subjectModel; 
        }

        public CourseModel CreateOrUpdate(CourseModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Course subject = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                subject = this.UnitOfWork.CourseRepository.CreateCourseSubject(model.TrainingProgramId, model.SubjectId, model.Name, model.ShortName, model.TotalSection, model.SectionPerWeek, model.IsTeachingByHomeroomTeacher, model.IsActive);
            }
            else
            {
                subject = this.UnitOfWork.CourseRepository.UpdateCourseSubject(model.Id, model.SubjectId, model.Name, model.ShortName, model.TotalSection, model.SectionPerWeek, model.IsTeachingByHomeroomTeacher, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            CourseModel subjectModel = Mapper.Map<Models.Course, Models.CourseModel>(subject);

            return subjectModel;
        }
    }
}
