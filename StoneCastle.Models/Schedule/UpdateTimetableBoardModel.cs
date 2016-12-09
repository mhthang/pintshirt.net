using System;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    public class UpdateTimetableBoardModel
    {
        public UpdateTimetableBoardModel()
        {
        }       

        public ICollection<CourseSectionModel> CourseSections { get; set; }
    }
}
