using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("TeacherDivisions")]
    public class TeacherDivision : IEntity<System.Guid>
    {
        public TeacherDivision()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public System.Guid DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }

        public System.Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}
