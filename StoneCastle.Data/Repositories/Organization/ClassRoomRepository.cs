using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StoneCastle.Organization.Repositories
{
    public class ClassRoomRepository : Repository<Organization.Models.ClassRoom, System.Guid>, IClassRoomRepository
    {
        public ClassRoomRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.ClassRoom> SearchClassRoom(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.ClassRoom> SearchSemesterClasses(Guid semesterId, Guid? classGroupId, string filter, int pageIndex, int pageSize, ref int total)
        {
            var classes = this.GetAll().Where(x => 
                (x.ClassGroup != null && x.ClassGroup.SemesterId == semesterId) && 
                (classGroupId == null || x.ClassGroupId == classGroupId) && 
                (string.IsNullOrWhiteSpace(filter) || x.Name.Contains(filter.Trim()) || (x.HomeroomTeacher != null &&
                    $"{x.HomeroomTeacher.Account.Profile.FirstName} {x.HomeroomTeacher.Account.Profile.LastName}".Contains(filter.Trim()))))
                .OrderBy(x => x.Name)
                .ToList();
            total = classes.Count;
            return classes.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Models.ClassRoom CreateClassRoom(Guid classGroupId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (classGroupId == null || classGroupId == System.Guid.Empty)
                throw new ArgumentNullException("classGroupId");

            Models.ClassRoom room = new Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                ClassGroupId = classGroupId,
                Name = name,
                Code = shortName,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.ClassRoom>(room);

            return room;
        }

        public Models.ClassRoom UpdateClassRoom(Guid id, Guid classGroupId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.ClassRoom room = this.GetById(id);

            if (room == null)
                throw new InvalidOperationException($"Room ({id}) does not exist.");

            room.Name = name;
            room.ClassGroupId = classGroupId;
            room.Code = shortName;
            room.HighlightColor = HighlightColor;
            room.LogoUrl = logoUrl;
            room.IsActive = isActive;

            this.DataContext.Update<Models.ClassRoom, Guid>(room, x => x.Name, x=>x.Code, x=>x.ClassGroupId, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return room;
        }

        public bool UpdateHomeroomTeacher(Guid classRoomId, Guid? teacherId)
        {
            var classRoom = this.GetById(classRoomId);
            if (classRoom != null)
            {
                if (teacherId == null)
                {
                    classRoom.TeacherId = null;
                    this.DataContext.Update<Models.ClassRoom, Guid>(classRoom, x => x.TeacherId);
                    return true;
                }
                else
                {
                    var teacher = DataContext.FindById<Account.Models.Teacher>(teacherId);
                    if (teacher != null)
                    {
                        classRoom.TeacherId = teacher.Id;
                        this.DataContext.Update<Models.ClassRoom, Guid>(classRoom, x => x.TeacherId);
                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Teacher ({teacherId}) does not exist.");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"Room ({classRoomId}) does not exist.");
            }
        }

        public int CountSemesterClassRoom(Guid semesterId)
        {
            List<Guid> groups = this.DataContext.Get<Models.ClassGroup>().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();

            return this.GetAll().Count(x => x.ClassGroupId.HasValue && groups.Contains(x.ClassGroupId.Value) && x.IsActive && !x.IsDeleted);
        }

    }
}
