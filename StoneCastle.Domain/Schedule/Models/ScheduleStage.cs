using StoneCastle.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    public enum SCHEDULE_STAGE
    {
        NEW = 0,
        VALIDATED = 1,
        GENERATING = 2,
        GENERATED = 3,
        ADJUSTMENT = 4,
        COMPLETED = 5,
        CANCEL = 6
    }

    [Table("ScheduleStages")]
    public class ScheduleStage : IEntity<System.Int32>
    {
        public ScheduleStage()
        {
        }

        [Key]
        public System.Int32 Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }
    }
}
