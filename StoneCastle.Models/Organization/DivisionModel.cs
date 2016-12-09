using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class DivisionModel
    {
        public DivisionModel()
        {
        }

        public System.Guid Id { get; set; }

        public System.Guid SemesterId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }

    }
}
