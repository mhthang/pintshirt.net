using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.TrainingProgram.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
            //Clients = new HashSet<Client>();
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "ShortName cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public bool IsActive { get; set; }

        public System.Guid TrainingProgramId { get; set; }
        public TrainingProgramModel TrainingProgram { get; set; }

        public System.Guid SubjectId { get; set; }
        public SubjectModel Subject { get; set; }


        public System.Guid? TimetableId { get; set; }
        //public Timetable Timetable { get; set; }

        //public virtual IEnumerable<Client> Clients { get; set; }

        public int TotalSection { get; set; }
        public int SectionPerWeek { get; set; }
        public bool IsTeachingByHomeroomTeacher { get; set; }

        public string HighlightColor { get; set; }

        public bool IsDeleted { get; set; }
    }
}
