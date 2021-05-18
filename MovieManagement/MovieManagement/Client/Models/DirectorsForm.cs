using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Client.Models
{
    public class DirectorsForm
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
    }
}
