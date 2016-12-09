using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Domain.Models
{
    [Serializable]
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
