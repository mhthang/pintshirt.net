using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Linq;
using System.Collections.Generic;
using System;

namespace StoneCastle.Organization.Repositories
{
    public class RoomRepository : Repository<Organization.Models.Room, System.Guid>, IRoomRepository
    {
        public RoomRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Room> SearchRoom(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Room> SearchSemesterRoom(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            var query = this.GetAll().Where(x => x.Building.SemesterId == semesterId);
            return query.ToList();
        }

        public Models.Room CreateRoom(Guid buildingId, string name, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Models.Room room = new Models.Room()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                Name = name,
                LogoUrl = logoUrl,
                HighlightColor = HighlightColor,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Room>(room);

            return room;
        }

        public Models.Room UpdateRoom(Guid id, Guid buildingId, string name, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.Room room = this.GetById(id);

            if (room == null)
                throw new InvalidOperationException($"Room ({id}) does not exist.");

            room.Name = name;
            room.BuildingId = buildingId;
            room.LogoUrl = logoUrl;
            room.HighlightColor = HighlightColor;
            room.IsActive = isActive;

            this.DataContext.Update<Models.Room, Guid>(room, x => x.Name, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return room;
        }

        public int CountSemesterRoom(Guid semesterId)
        {
            List<Guid> buildings = this.DataContext.Get<Models.Building>().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();

            return this.GetAll().Count(x => buildings.Contains(x.BuildingId) && x.IsActive == true && x.IsDeleted == false);
        }
    }
}
