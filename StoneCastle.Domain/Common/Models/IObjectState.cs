using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Domain.Models
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }
}