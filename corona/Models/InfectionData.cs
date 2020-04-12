using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace corona.Models
{
    public class InfectionData
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public int ConfirmedCases { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public int Deaths { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public int Recovered { get; set; }

        [Display(Name = "Country Name")]
        public int CountryId { get; set; }
        
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}