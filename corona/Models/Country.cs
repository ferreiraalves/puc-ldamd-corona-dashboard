using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace corona.Models
{
    [Table("Countries")]
    
    public class Country
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }
        
        public InfectionData InfectionData { get; set; }
        

    }
}