using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Organization.Services
{
    public interface IRoomService : IBaseService
    {
        SearchResponse<RoomModel> SearchRoom(SearchRequest request);
        SearchResponse<RoomModel> SearchSemesterRoom(SearchRequest request);
        RoomModel GetRoom(RoomModel model);
        RoomModel CreateOrUpdate(RoomModel model);
    }
}
