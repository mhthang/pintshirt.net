using System;

namespace StoneCastle.Domain.Models
{
    public interface IEntityTrackingModified
    {
        DateTime DateModified { set; }
    }
}
