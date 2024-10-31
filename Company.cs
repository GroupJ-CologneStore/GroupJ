using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CologneStore.Models
{
    public class Company
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter your company name")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter your company registration number")]
        public string? RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Please enter your company address")]
        public string? CompanyAddress { get; set; }
        public string? CIPCImage { get; set; }
        
    }
}
