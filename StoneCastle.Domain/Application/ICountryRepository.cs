using StoneCastle.Domain;
using StoneCastle.Application.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Application
{
    public interface ICountryRepository : IRepository<Country, System.Int32>
    {
    }
}
