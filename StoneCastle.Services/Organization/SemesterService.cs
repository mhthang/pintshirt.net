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
    public class SemesterService : BaseService, ISemesterService
    {
        public SemesterService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<SemesterModel> SearchSemester(SearchRequest request)
        {
            Logger.Debug($"Search: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<SemesterModel> response = new SearchResponse<SemesterModel>();

            List<Models.Semester> semesters = this.UnitOfWork.SemesterRepository.SearchSemesterFromOrg(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var semesterModels = Mapper.Map<List<Models.Semester>, List<Models.SemesterModel>>(semesters);
            response.Records = semesterModels;
            response.Total = semesterModels.Count;
            response.Pager = request.Pager;

            return response;
        }


        public SearchResponse<SemesterModel> GetAllSemesters()
        {
            SearchResponse<SemesterModel> response = new SearchResponse<SemesterModel>();

            List<Models.Semester> semesters = this.UnitOfWork.SemesterRepository.GetAllSemesters();

            var semesterModels = Mapper.Map<List<Models.Semester>, List<Models.SemesterModel>>(semesters);
            response.Records = semesterModels;
            response.Total = semesterModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }
        public SemesterModel GetSemester(SemesterModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Semester semester = this.UnitOfWork.SemesterRepository.GetById(model.Id);

            SemesterModel semesterModel = Mapper.Map<Models.Semester, Models.SemesterModel>(semester);

            return semesterModel; 
        }

        public SemesterModel GetSemesterWithOrganization(SemesterModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Semester semester = this.UnitOfWork.SemesterRepository.GetById(model.Id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Organization.Models.Organization, Organization.Models.OrganizationModel>().ForMember(x => x.Semesters, y => y.Ignore());
                cfg.CreateMap<Organization.Models.Semester, Organization.Models.SemesterModel>();
            });

            var mapper = config.CreateMapper();

            SemesterModel semesterModel = mapper.Map<Models.Semester, Models.SemesterModel>(semester);

            return semesterModel;
        }

        public SemesterModel CreateOrUpdate(SemesterModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Semester semester = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                semester = this.UnitOfWork.SemesterRepository.CreateSemester(model.OrganizationId, model.Name, model.ShortName, model.HighlightColor, model.IsActive);
            }
            else
            {
                semester = this.UnitOfWork.SemesterRepository.UpdateSemester(model.Id, model.Name, model.ShortName, model.HighlightColor, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            SemesterModel semesterModel = Mapper.Map<Models.Semester, Models.SemesterModel>(semester);

            return semesterModel;
        }

        public SemesterSummaryModel GetSummary(Guid semesterId)
        {
            Logger.Debug($"{semesterId}");

            if (semesterId == null || semesterId == Guid.Empty)
                throw new System.ArgumentNullException("semesterId");

            Semester semester = this.UnitOfWork.SemesterRepository.GetById(semesterId);

            if (semester == null)
            {
                throw new InvalidOperationException($"Semester Id {semesterId} does not exist");
            }


            SemesterSummaryModel semesterModel = Mapper.Map<Models.Semester, Models.SemesterSummaryModel>(semester);

            semesterModel.TotalBuilding = this.UnitOfWork.BuildingRepository.CountSemesterBuilding(semesterId);
            semesterModel.TotalRoom = this.UnitOfWork.RoomRepository.CountSemesterRoom(semesterId);
            semesterModel.TotalProgram = this.UnitOfWork.TrainingProgramRepository.CountSemesterProgram(semesterId);
            semesterModel.TotalClassRoom = this.UnitOfWork.ClassRoomRepository.CountSemesterClassRoom(semesterId);
            semesterModel.TotalDivision = this.UnitOfWork.DivisionRepository.CountSemesterDivision(semesterId);
            semesterModel.TotalTeacher = this.UnitOfWork.TeacherRepository.CountSemesterTeacher(semesterId);
            semesterModel.TotalSubjectGroup = this.UnitOfWork.SubjectGroupRepository.CountSemesterSubjectGroup(semesterId);
            semesterModel.TotalSubject = this.UnitOfWork.SubjectRepository.CountSemesterSubject(semesterId);
            semesterModel.TotalCourse = this.UnitOfWork.ClassCourseRepository.CountSemesterCourse(semesterId);


            return semesterModel;
        }

    }
}
