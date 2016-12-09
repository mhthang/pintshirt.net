using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;
using System.Linq;
using System;

namespace StoneCastle.Organization.Services
{
    public class ClassCourseService : BaseService, IClassCourseService
    {
        public ClassCourseService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<ClassCourseModel> SearchClassCourse(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");

            SearchResponse<ClassCourseModel> response = new SearchResponse<ClassCourseModel>();

            List<Models.ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.SearchCourse(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var courseModels = Mapper.Map<List<Models.ClassCourse>, List<Models.ClassCourseModel>>(courses);
            response.Records = courseModels;
            response.Total = courseModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<CourseListModel> SearchSemesterClassCourse(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");

            SearchResponse<CourseListModel> response = new SearchResponse<CourseListModel>();

            List<Models.ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.SearchSemesterCourse(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var courseModels = Mapper.Map<List<Models.ClassCourse>, List<Models.CourseListModel>>(courses);
            response.Records = courseModels;
            response.Total = courseModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public ClassCourseModel GetClassCourse(ClassCourseModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            ClassCourse course = this.UnitOfWork.ClassCourseRepository.GetById(model.Id);

            ClassCourseModel courseModel = Mapper.Map<Models.ClassCourse, Models.ClassCourseModel>(course);

            return courseModel; 
        }

        public IEnumerable<ClassCourseModel> GetClassCoursesByClassRoomId(System.Guid id)
        {
            Logger.Debug($"Class Room Id = {id}");

            if (id == null || id == System.Guid.Empty)
                throw new System.ArgumentNullException("id");

            IEnumerable<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(id);

            IEnumerable<ClassCourseModel> coursesModel = Mapper.Map<IEnumerable<ClassCourse>, IEnumerable<ClassCourseModel>>(courses);

            return coursesModel;
        }

        public ClassCourseModel CreateOrUpdate(ClassCourseModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            ClassCourse course = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                course = this.UnitOfWork.ClassCourseRepository.CreateCourse(model.ClassRoomId, model.CourseId, model.TeacherId, model.RoomId, model.IsActive);
            }
            else
            {
                course = this.UnitOfWork.ClassCourseRepository.UpdateCourse(model.Id, model.ClassRoomId, model.CourseId, model.TeacherId, model.RoomId, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            ClassCourseModel courseModel = Mapper.Map<Models.ClassCourse, Models.ClassCourseModel>(course);

            return courseModel;
        }

        public int GenerateSemesterClassCourse(System.Guid id)
        {
            int count = 0;

            Logger.Debug($"SemesterId = {id}");

            if (id == null || id == System.Guid.Empty)
                throw new System.ArgumentNullException("id");

            IEnumerable<ClassGroup> classGroups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(id);

            foreach(ClassGroup group in classGroups.ToList())
            {
                IEnumerable<ClassRoom> classRooms = group.ClassRooms;

                TrainingProgram.Models.TrainingProgram program = group.TrainingProgram;

                IEnumerable<TrainingProgram.Models.Course> subjects = program.CourseSubjects;
                List<Guid> programSubjectIds = subjects.Select(x => x.Id).ToList();

                foreach(ClassRoom cls in classRooms)
                {
                    IEnumerable<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(cls.Id);
                    List<Guid?> courseSubjectIds = courses.Select(x => x.CourseId).ToList();

                    List<Guid> needToAddedSubjectIdList = new List<Guid>();
                    foreach(Guid subjectId in programSubjectIds)
                    {
                        if(courseSubjectIds.IndexOf(subjectId) < 0)
                        {
                            needToAddedSubjectIdList.Add(subjectId);
                        }
                    }

                    foreach(Guid subjectId in needToAddedSubjectIdList)
                    {
                        this.UnitOfWork.ClassCourseRepository.AddCourse(cls.Id, subjectId);
                        count++;
                    }
                }

            }

            if(count > 0)
            {
                this.UnitOfWork.SaveChanges();
            }

            return count;
        }
    }
}
