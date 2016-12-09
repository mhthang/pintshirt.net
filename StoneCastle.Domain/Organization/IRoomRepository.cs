using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IRoomRepository : IRepository<Room, System.Guid>
    {
        List<Models.Room> SearchRoom(string filter, int pageIndex, int pageSize);
        List<Models.Room> SearchSemesterRoom(Guid semesterId, string filter, int pageIndex, int pageSize);

        Models.Room CreateRoom(Guid buildingId, string name, string HighlightColor, string logoUrl, bool isActive);
        Models.Room UpdateRoom(Guid id, Guid buildingId, string name, string HighlightColor, string logoUrl, bool isActive);

        int CountSemesterRoom(Guid semesterId);
    }
}
