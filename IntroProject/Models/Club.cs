using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroProject.Models
{
    public class Club
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Membership> Memberships { get; set; }
    }
}
