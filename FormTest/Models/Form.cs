using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FormTest.Models
{
    public class Form
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Phone]
        public string MobilePhone { get; set; }
        [Required]
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Post { get; set; }
    }
}