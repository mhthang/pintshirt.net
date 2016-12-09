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
    public interface IClassRoomService : IBaseService
    {
        SearchResponse<ClassRoomModel> SearchClassRoom(SearchRequest request);
        ClassRoomModel GetClassRoom(ClassRoomModel model);
        ClassRoomModel CreateOrUpdate(ClassRoomModel model);
        bool CreateOrUpdateClassRoomTeacher(UpdateForeignKeyRequest request);
    }
}
