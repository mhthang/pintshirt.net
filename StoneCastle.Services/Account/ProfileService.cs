using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;


namespace StoneCastle.Account.Services
{
    public class ProfileService : BaseService, IProfileService
    {
        public ProfileService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public Models.ProfileModel t()
        {
            Logger.Info($"Instance Created");

            Account.Models.Profile p = new Models.Profile()
            {
                FirstName = "System",
                LastName = "Admin",
                Email = "admin@stonecastle.com"
            };

            var dest = Mapper.Map<Models.Profile, Models.ProfileModel>(p);

            return dest;
        }
    }
}
