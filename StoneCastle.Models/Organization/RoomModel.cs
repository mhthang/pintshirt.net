using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class RoomModel
    {
        public RoomModel()
        {
        }

        public System.Guid Id { get; set; }

        public System.Guid BuildingId { get; set; }
        public BuildingModel Building { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }

    }
}
