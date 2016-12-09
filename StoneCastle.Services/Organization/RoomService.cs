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
    public class RoomService : BaseService, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public SearchResponse<RoomModel> SearchRoom(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");


            SearchResponse<RoomModel> response = new SearchResponse<RoomModel>();

            List<Models.Room> rooms = this.UnitOfWork.RoomRepository.SearchRoom(request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var roomModels = Mapper.Map<List<Models.Room>, List<Models.RoomModel>>(rooms);
            response.Records = roomModels;
            response.Total = roomModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public SearchResponse<RoomModel> SearchSemesterRoom(SearchRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            if (request.Pager == null)
                request.Pager = this.GetDefaultPager();

            Logger.Debug($"Search: {request.FilterText}");
            Logger.Debug($"Search Semester: {request.Id}");


            SearchResponse<RoomModel> response = new SearchResponse<RoomModel>();

            List<Models.Room> rooms = this.UnitOfWork.RoomRepository.SearchSemesterRoom(request.Id, request.FilterText, request.Pager.PageIndex, request.Pager.PageSize);

            var roomModels = Mapper.Map<List<Models.Room>, List<Models.RoomModel>>(rooms);
            response.Records = roomModels;
            response.Total = roomModels.Count;
            response.Pager = request.Pager;

            return response;
        }

        public RoomModel GetRoom(RoomModel model)
        {
            Logger.Debug($"{model}");

            if (model == null || model.Id == null || model.Id == System.Guid.Empty)
                throw new System.ArgumentNullException("model");

            Room room = this.UnitOfWork.RoomRepository.GetById(model.Id);

            RoomModel roomModel = Mapper.Map<Models.Room, Models.RoomModel>(room);

            return roomModel;
        }

        public RoomModel CreateOrUpdate(RoomModel model)
        {
            Logger.Debug($"{model}");

            if (model == null)
                throw new System.ArgumentNullException("model");

            Room room = null;

            if (model.Id == null || model.Id == System.Guid.Empty)
            {
                room = this.UnitOfWork.RoomRepository.CreateRoom(model.BuildingId, model.Name, model.HighlightColor, model.LogoUrl, model.IsActive);
            }
            else
            {
                room = this.UnitOfWork.RoomRepository.UpdateRoom(model.Id, model.BuildingId, model.Name, model.HighlightColor, model.LogoUrl, model.IsActive);
            }

            this.UnitOfWork.SaveChanges();

            RoomModel roomModel = Mapper.Map<Models.Room, Models.RoomModel>(room);

            return roomModel;
        }
    }
}
