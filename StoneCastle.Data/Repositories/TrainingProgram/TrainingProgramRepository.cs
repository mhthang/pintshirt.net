using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.TrainingProgram.Repositories
{
    public class TrainingProgramRepository : Repository<TrainingProgram.Models.TrainingProgram, System.Guid>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.TrainingProgram> SearchTrainingProgram(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.TrainingProgram> SearchSemesterTrainingProgram(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public List<Models.TrainingProgram> GetSemesterTrainingProgram(Guid semesterId)
        {
            return this.GetAll().Where(x=>x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public Models.TrainingProgram CreateTrainingProgram(Guid semesterId, string name, string code, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Models.TrainingProgram program = new Models.TrainingProgram()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                Name = name,
                Code = code,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.TrainingProgram>(program);

            return program;
        }


        public Models.TrainingProgram UpdateTrainingProgram(Guid id, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.TrainingProgram program = this.GetById(id);

            if (program == null)
                throw new InvalidOperationException($"Division ({id}) does not exist.");

            program.Name = name;
            program.Code = shortName;
            program.HighlightColor = HighlightColor;
            program.LogoUrl = logoUrl;
            program.IsActive = isActive;

            this.DataContext.Update<Models.TrainingProgram, Guid>(program, x => x.Name, x=>x.Code, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return program;
        }

        public Timetable GetTimetable(Guid programId)
        {
            if (programId == null)
                throw new ArgumentNullException("programId");

            Timetable tt = this.GetAll().Where(x => x.Id == programId).Select(x => x.Timetable).FirstOrDefault();

            return tt;
        }

        public Timetable CreateTimetable(Guid programId, int shiftPerDay, int slotPerShift)
        {
            if (programId == null)
                throw new ArgumentNullException("programId");

            Timetable tt = this.GetAll().Where(x => x.Id == programId).Select(x => x.Timetable).FirstOrDefault();

            if (tt == null)
            {
                tt = new Timetable()
                {
                    Id = Guid.NewGuid(),
                    ShiftPerDay = shiftPerDay,
                    SlotPerShift = slotPerShift,
                    HighlightColor = Commons.Ultility.GetHighlightColor(new Random())
                };

                TrainingProgram.Models.TrainingProgram program = this.GetById(programId);
                if (program == null)
                    throw new InvalidOperationException($"Program ({programId}) does not exist.");

                program.TimetableId = tt.Id;
                this.DataContext.Insert<Timetable>(tt);
                this.DataContext.Update<TrainingProgram.Models.TrainingProgram, Guid>(program, x => x.TimetableId);
            }

            return tt;
        }

        public int CountSemesterProgram(Guid semesterId)
        {
            return this.GetAll().Count(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted);
        }
    }
}
