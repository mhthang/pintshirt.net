using StoneCastle.Organization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    public class SchedulingTableModel
    {
        public SchedulingTableModel()
        {
            ClassTimetables = new HashSet<ClassTimetableModel>();
            ScheduleEvents = new HashSet<ScheduleEventModel>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Short Name cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public string LogoUrl { get; set; }
        public string HighlightColor { get; set; }

        public SCHEDULE_STAGE Stage { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime AddedStamp { get; set; }
        public System.Guid? AddedUserId { get; set; }
        public DateTime? ChangedStamp { get; set; }
        public System.Guid? ChangedUserId { get; set; }

        public System.Guid SemesterId { get; set; }
        public SemesterModel Semester { get; set; }

        public ICollection<ScheduleEventModel> ScheduleEvents { get; set; }
        public IEnumerable<ClassTimetableModel> ClassTimetables { get; set; }
    }
}
