using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using System.Collections.Generic;
using StoneCastle.Organization.Models;
using StoneCastle.TrainingProgram.Models;
using System;

namespace StoneCastle.TrainingProgram.Services
{
    public class TrainingProgramService : BaseService, ITrainingProgramService
    {
        public TrainingProgramService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<TrainingProgramModel> SearchTrainingProgram(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            Logger.Debug($"Search: {request.FilterText}");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<TrainingProgramModel> response = new SearchResponse<TrainingProgramModel>();

            List<Models.TrainingProgram> programs = this.UnitOfWork.TrainingProgramRepository.SearchTrainingProgram(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);            

            var programModels = Mapper.Map<List<Models.TrainingProgram>, List<Models.TrainingProgramModel>>(programs);
            response.Records = programModels;
            response.Total = programModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<TrainingProgramModel> SearchSemesterTrainingProgram(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            Logger.Debug($"Search: {request.FilterText}");
            Logger.Debug($"Search Semester: {request.Id}");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<TrainingProgramModel> response = new SearchResponse<TrainingProgramModel>();

            List<Models.TrainingProgram> programs = this.UnitOfWork.TrainingProgramRepository.SearchSemesterTrainingProgram(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var programModels = Mapper.Map<List<Models.TrainingProgram>, List<Models.TrainingProgramModel>>(programs);
            response.Records = programModels;
            response.Total = programModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public TrainingProgramModel GetTrainingProgram(TrainingProgramModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            TrainingProgram.Models.TrainingProgram program = this.UnitOfWork.TrainingProgramRepository.GetById(model.Id);

            TrainingProgramModel programModel = Mapper.Map<Models.TrainingProgram, Models.TrainingProgramModel>(program);

            return programModel; 
        }

        public List<TrainingProgramModel> GetSemesterPrograms(Guid semesterId)
        {
            Logger.Debug($"{semesterId}");

            if (semesterId == null || semesterId == System.Guid.Empty)
                throw new System.ArgumentNullException("semesterId");

            List<TrainingProgram.Models.TrainingProgram> programs = this.UnitOfWork.TrainingProgramRepository.GetSemesterTrainingProgram(semesterId);

            List<TrainingProgramModel> programModels = Mapper.Map<List<Models.TrainingProgram>, List<Models.TrainingProgramModel>>(programs);

            return programModels;
        }

        public TrainingProgramModel CreateOrUpdate(TrainingProgramModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            TrainingProgram.Models.TrainingProgram program = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                program = this.UnitOfWork.TrainingProgramRepository.CreateTrainingProgram(model.SemesterId, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                program = this.UnitOfWork.TrainingProgramRepository.UpdateTrainingProgram(model.Id, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            TrainingProgramModel programModel = Mapper.Map<Models.TrainingProgram, Models.TrainingProgramModel>(program);

            return programModel;
        }
    }
}
