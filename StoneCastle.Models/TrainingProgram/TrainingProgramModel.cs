using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.TrainingProgram.Models
{
    public class TrainingProgramModel
    {
        public TrainingProgramModel()
        {
            CourseSubjects = new HashSet<CourseModel>();
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        public System.Guid SemesterId { get; set; }
        public Organization.Models.Semester Semester { get; set; }


        public string HighlightColor { get; set; }
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<CourseModel> CourseSubjects { get; set; }

        public System.Guid? WeeklyTimeTableId { get; set; }
        //public WeeklyTimeTable WeeklyTimeTable { get; set; }

        public bool IsDeleted { get; set; }
    }
}
