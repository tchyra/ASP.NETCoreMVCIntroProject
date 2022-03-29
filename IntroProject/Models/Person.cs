using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroProject.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public List<Membership> Memberships { get; set; }

        public static Person CreateNew(string firstName, string lastName, DateTime dateOfBirth)
        {
            return new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
            };
        }
    }
}
