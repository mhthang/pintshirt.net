using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.TrainingProgram.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class CourseListModel
    {
        public CourseListModel()
        {
        }

        public System.Guid Id { get; set; }

        public int SectionPerWeek { get; set; }

        public System.Guid? TeacherId { get; set; }
        public String TeacherName { get; set; }

        public System.Guid? ClassRoomId { get; set; }
        public String ClassRoomName { get; set; }

        public System.Guid? CourseSubjectId { get; set; }
        //public CourseSubjectModel CourseSubject { get; set; }

        public String SubjectName { get; set; }

        public System.Guid? RoomId { get; set; }
        public String RoomName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
