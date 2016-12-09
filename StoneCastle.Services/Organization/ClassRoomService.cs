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
    public class ClassRoomService : BaseService, IClassRoomService
    {
        public ClassRoomService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<ClassRoomModel> SearchClassRoom(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");

            SearchResponse<ClassRoomModel> response = new SearchResponse<ClassRoomModel>();

            int total = 0;
            List<Models.ClassRoom> rooms = this.UnitOfWork.ClassRoomRepository.SearchSemesterClasses(request.Id, request.FilterId, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize, ref total);            

            var models = Mapper.Map<List<Models.ClassRoom>, List<Models.ClassRoomModel>>(rooms);
            response.Records = models;
            response.Total = total;
            response.Pager = request.Pager;

            return response;
        }

        public ClassRoomModel GetClassRoom(ClassRoomModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            ClassRoom room = this.UnitOfWork.ClassRoomRepository.GetById(model.Id);

            ClassRoomModel roomModel = Mapper.Map<Models.ClassRoom, Models.ClassRoomModel>(room);

            return roomModel; 
        }

        public ClassRoomModel CreateOrUpdate(ClassRoomModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            ClassRoom room = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                room = this.UnitOfWork.ClassRoomRepository.CreateClassRoom(model.ClassGroupId.Value, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                room = this.UnitOfWork.ClassRoomRepository.UpdateClassRoom(model.Id, model.ClassGroupId.Value, model.Name, model.Code, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            ClassRoomModel roomModel = Mapper.Map<Models.ClassRoom, Models.ClassRoomModel>(room);

            return roomModel;
        }

        public bool CreateOrUpdateClassRoomTeacher(UpdateForeignKeyRequest request)
        {
            Logger.Debug($"Add teacher {request.ForeignKeyId} to class room {request.PrimaryKeyId}");

            var result = this.UnitOfWork.ClassRoomRepository.UpdateHomeroomTeacher(request.PrimaryKeyId, request.ForeignKeyId);

            this.UnitOfWork.SaveChanges();

            return result;
        }
    }
}
