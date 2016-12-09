using StoneCastle.Organization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StoneCastle.Schedule.Models
{
    public class ScheduleStageInfo
    {
        public ScheduleStageInfo()
        {
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

        public bool CanValidate
        {
            get
            {
                if (this.Stage != SCHEDULE_STAGE.COMPLETED && this.Stage != SCHEDULE_STAGE.CANCEL)
                    return true;

                return false;
            }
        }

        public bool CanGenerate
        {
            get
            {
                if (this.Stage == SCHEDULE_STAGE.VALIDATED)
                    return true;

                return false;
            }
        }

        public bool CanAdjust
        {
            get
            {
                if (this.Stage == SCHEDULE_STAGE.GENERATED)
                    return true;

                return false;
            }
        }

        public bool CanComplete
        {
            get
            {
                if (this.Stage == SCHEDULE_STAGE.GENERATED || this.Stage == SCHEDULE_STAGE.ADJUSTMENT)
                    return true;

                return false;
            }
        }


        public DateTime? CreateStamp
        {
            get
            {
                if (this.ScheduleEvents != null && this.ScheduleEvents.Count > 0)
                {
                    ScheduleEventModel createEvent = this.ScheduleEvents.Where(x=>x.Stage == SCHEDULE_STAGE.NEW).FirstOrDefault();
                    if (createEvent != null)
                        return createEvent.Timestamp;
                }

                return null;
            }
        }

        public DateTime? LastValidateStamp
        {
            get
            {
                if (this.ScheduleEvents != null && this.ScheduleEvents.Count > 0)
                {
                    ScheduleEventModel createEvent = this.ScheduleEvents.Where(x => x.Stage == SCHEDULE_STAGE.VALIDATED).OrderByDescending(x=>x.Timestamp).FirstOrDefault();
                    if (createEvent != null)
                        return createEvent.Timestamp;
                }

                return null;
            }
        }

        public DateTime? LastGenerateStamp
        {
            get
            {
                if (this.ScheduleEvents != null && this.ScheduleEvents.Count > 0)
                {
                    ScheduleEventModel createEvent = this.ScheduleEvents.Where(x => x.Stage == SCHEDULE_STAGE.GENERATED).OrderByDescending(x => x.Timestamp).FirstOrDefault();
                    if (createEvent != null)
                        return createEvent.Timestamp;
                }

                return null;
            }
        }
        public DateTime? LastAdjustStamp
        {
            get
            {
                if (this.ScheduleEvents != null && this.ScheduleEvents.Count > 0)
                {
                    ScheduleEventModel createEvent = this.ScheduleEvents.Where(x => x.Stage == SCHEDULE_STAGE.ADJUSTMENT).OrderByDescending(x => x.Timestamp).FirstOrDefault();
                    if (createEvent != null)
                        return createEvent.Timestamp;
                }

                return null;
            }
        }

        public DateTime? CompleteStamp
        {
            get
            {
                if (this.ScheduleEvents != null && this.ScheduleEvents.Count > 0)
                {
                    ScheduleEventModel createEvent = this.ScheduleEvents.Where(x => x.Stage == SCHEDULE_STAGE.COMPLETED).OrderByDescending(x => x.Timestamp).FirstOrDefault();
                    if (createEvent != null)
                        return createEvent.Timestamp;
                }

                return null;
            }
        }

        public System.Guid SemesterId { get; set; }

        public ICollection<ScheduleEventModel> ScheduleEvents { get; set; }
    }
}
