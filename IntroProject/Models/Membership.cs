using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntroProject.Models
{
    public class Membership
    {
        [ForeignKey(nameof(Club))]
        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        [ForeignKey(nameof(Person))]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        // this entity can have additional properties aside from being a connector between Club and Person!
        public DateTime DateCreated { get; set; }
    }
}
