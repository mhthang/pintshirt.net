using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class ClassGroupSchedule
    {
        public ClassGroupSchedule()
        {
            ClassRooms = new List<ClassRoomSchedule>();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string LogoUrl { get; set; }

        public System.Guid TrainingProgramId { get; set; }

        public TrainingProgramSchedule TrainingProgram { get; set; }

        public ICollection<ClassRoomSchedule> ClassRooms { get; set; }
    }
}
