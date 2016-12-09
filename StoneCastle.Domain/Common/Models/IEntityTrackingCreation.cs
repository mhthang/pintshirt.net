using System;

namespace StoneCastle.Domain.Models
{
    public interface IEntityTrackingCreation
    {
        DateTime DateCreated { set; }
    }
}
