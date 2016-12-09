using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StoneCastle.Account.Services
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchTeacherResponse SearchTeachers(SearchRequest request)
        {
            Logger.Debug($"Search Teachers: {request.FilterText}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchTeacherResponse response = new SearchTeacherResponse();

            List<Models.Teacher> teachers = this.UnitOfWork.TeacherRepository.SearchSemesterTeacher(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var teacherModels = Mapper.Map<List<Models.Teacher>, List<Models.TeacherModel>>(teachers);
            response.Records = teacherModels;
            response.Total = teacherModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public TeacherViewModel GetTeacher(TeacherModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Teacher teacher = this.UnitOfWork.TeacherRepository.GetById(model.Id);

            Organization.Models.TeacherDivision teacherDivision = teacher.TeacherDivisions.FirstOrDefault();

            TeacherModel teacherModel = Mapper.Map<Models.Teacher, Models.TeacherModel>(teacher);

            TeacherViewModel teacherViewModel = new TeacherViewModel()
            {
                Id = teacherModel.Id,
                DivisionId = teacherDivision != null ? teacherDivision.DivisionId : Guid.Empty,
                AccountId = teacherModel.AccountId,
                ProfileId = teacherModel.Account.ProfileId,
                FirstName = teacherModel.Account.Profile.FirstName,
                LastName = teacherModel.Account.Profile.LastName,
                Email = teacherModel.Account.Profile.Email,
                Phone = teacherModel.Account.Profile.Phone,
                HighlightColor = teacherModel.Account.Profile.HighlightColor,
                IsActive = teacher.IsActive
            };

            return teacherViewModel; 
        }

        public IEnumerable<TeacherViewModel> GetSemesterTeachers(SearchRequest request)
        {
            Logger.Debug($"Get Semester Teachers: {request.Id}");

            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            IEnumerable<Teacher> teachers = this.UnitOfWork.TeacherRepository.SearchSemesterTeacher(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            IEnumerable<TeacherViewModel> teachersViewModel = teachers.Select(x => new TeacherViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                ProfileId = x.Account.ProfileId,
                FirstName = x.Account.Profile.FirstName,
                LastName = x.Account.Profile.LastName,
                Email = x.Account.Profile.Email,
                Phone = x.Account.Profile.Phone,
                HighlightColor = x.Account.Profile.HighlightColor,
                IsActive = x.IsActive
            });

            return teachersViewModel;
        }

        public IEnumerable<TeacherViewModel> GetAvailableSemesterHomeroomTeachers(SearchRequest request)
        {
            Logger.Debug($"Get Available Semester Homeroom Teachers: {request.Id}");

            if (request == null)
                throw new System.ArgumentNullException("request");
            
            IEnumerable<Teacher> teachers = this.UnitOfWork.TeacherRepository.GetAvailableSemesterHomeroomTeachers(request.Id);

            IEnumerable<TeacherViewModel> teachersViewModel = teachers.Select(x => new TeacherViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                ProfileId = x.Account.ProfileId,
                FirstName = x.Account.Profile.FirstName,
                LastName = x.Account.Profile.LastName,
                Email = x.Account.Profile.Email,
                Phone = x.Account.Profile.Phone,
                HighlightColor = x.Account.Profile.HighlightColor,
                IsActive = x.IsActive
            });

            return teachersViewModel;
        }

        public TeacherModel CreateOrUpdate(TeacherViewModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Teacher teacher = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                teacher = this.UnitOfWork.TeacherRepository.CreateTeacher(model.DivisionId, model.FirstName, model.LastName, model.Email, model.Phone, model.HighlightColor, model.IsActive);
            }
            else
            {
                teacher = this.UnitOfWork.TeacherRepository.UpdateTeacher(model.Id, model.DivisionId, model.FirstName, model.LastName, model.Email, model.Phone, model.HighlightColor, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            TeacherModel teacherModel = Mapper.Map<Models.Teacher, Models.TeacherModel>(teacher);

            return teacherModel;
        }
    }
}
