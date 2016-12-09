using StoneCastle.Domain;
using StoneCastle.Application.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Application
{
    public interface ITimezoneRepository : IRepository<Timezone, System.Int32>
    {
    }
}
