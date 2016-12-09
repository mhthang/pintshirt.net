using StoneCastle.Schedule.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Schedule.Services
{
    public interface IScheduleService : IBaseService
    {
        ScheduleStageInfo GetScheduleStageInfo(Guid id);
        ScheduleStageInfo GetSemesterScheduleStageInfo(Guid semsterId);

        SearchResponse<SchedulingTableModel> SearchSemesterSchedule(SearchRequest request);
        SchedulingTableModel GetSchedule(Guid id);
        SchedulingTableModel CreateOrUpdate(SchedulingTableModel model);
    }
}
