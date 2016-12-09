using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("SchedulingTables")]
    public class SchedulingTable : IEntity<System.Guid>
    {
        public SchedulingTable()
        {
            Stage = SCHEDULE_STAGE.NEW;
            ClassTimetables = new HashSet<ClassTimetable>();
            ScheduleEvents = new HashSet<ScheduleEvent>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Short Name cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }
        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public SCHEDULE_STAGE Stage { get; set; }

        public System.Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public virtual IEnumerable<ClassTimetable> ClassTimetables { get; set; }
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime AddedStamp { get; set; }
        public System.Guid? AddedUserId { get; set; }
        public DateTime? ChangedStamp { get; set; }
        public System.Guid? ChangedUserId { get; set; }
    }
}
