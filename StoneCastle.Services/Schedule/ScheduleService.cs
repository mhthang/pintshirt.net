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
using StoneCastle.Schedule.Models;

namespace StoneCastle.Schedule.Services
{
    public class ScheduleService : BaseService, IScheduleService
    {
        public ScheduleService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }
        
        public SearchResponse<SchedulingTableModel> SearchSemesterSchedule(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            Logger.Debug($"Search: {request.FilterText}");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            SearchResponse<SchedulingTableModel> response = new SearchResponse<SchedulingTableModel>();

            IEnumerable<Models.SchedulingTable> schedules = this.UnitOfWork.SchedulingTableRepository.SearchSemesterSchedule(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var scheduleModels = Mapper.Map<List<Models.SchedulingTable>, List<Models.SchedulingTableModel>>(schedules.ToList());
            response.Records = scheduleModels;
            response.Total = scheduleModels.Count;
            response.Pager = this.GetDefaultPager();

            return response;
        }

        public SchedulingTableModel GetSchedule(Guid id)
        {
            Logger.Debug($"{id}");

            if (id == null || id == System.Guid.Empty)
                throw new System.ArgumentNullException("id");

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(id);

            SchedulingTableModel scheduleModel = Mapper.Map<Models.SchedulingTable, Models.SchedulingTableModel>(schedule);

            return scheduleModel; 
        }

        public SchedulingTableModel CreateOrUpdate(SchedulingTableModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            SchedulingTable schedule = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                schedule = this.UnitOfWork.SchedulingTableRepository.CreateSchedule(model.SemesterId, model.Name, model.ShortName, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                schedule = this.UnitOfWork.SchedulingTableRepository.UpdateSchedule(model.Id, model.SemesterId, model.Name, model.ShortName, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            SchedulingTableModel scheduleModel = Mapper.Map<Models.SchedulingTable, Models.SchedulingTableModel>(schedule);

            return scheduleModel;
        }

        public ScheduleStageInfo GetScheduleStageInfo(Guid id)
        {
            Logger.Debug($"{id}");

            if (id == null || id == System.Guid.Empty)
                throw new System.ArgumentNullException("id");

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(id);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Models.SchedulingTable, Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

        public ScheduleStageInfo GetSemesterScheduleStageInfo(Guid semsterId)
        {
            Logger.Debug($"{semsterId}");

            if (semsterId == null || semsterId == System.Guid.Empty)
                throw new System.ArgumentNullException("semsterId");


            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetSemesterDefaultSchedule(semsterId);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Models.SchedulingTable, Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

    }
}
