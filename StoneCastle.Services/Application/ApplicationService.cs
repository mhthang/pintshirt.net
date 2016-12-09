using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Services;
using log4net;
using StoneCastle.Application.Models;
using System.Collections.Generic;
using System.Linq;

namespace StoneCastle.Application.Services
{
    public class ApplicationService : BaseService, IApplicationService
    {
        public ApplicationService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public List<Application.Models.CountryModel> GetCountries()
        {
            List<Application.Models.Country> countries = this.UnitOfWork.CountryRepository.GetAll().ToList();
            List<Application.Models.CountryModel> countryModels = Mapper.Map<List<Country>, List<CountryModel>>(countries);

            return countryModels;
        }

        public List<Application.Models.TimezoneModel> GetTimezones()
        {
            List<Timezone> timezones = this.UnitOfWork.TimezoneRepository.GetAll().ToList();
            List<TimezoneModel> timezoneModels = Mapper.Map<List<Timezone>, List<TimezoneModel>>(timezones);

            return timezoneModels;
        }

    }
}
