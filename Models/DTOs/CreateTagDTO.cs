using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotasApi.Models.DTOs
{
    public class CreateTagDTO
    {
        [Required]
        [MaxLength(60, ErrorMessage = "Max 60 characters")]
        public string Name { get; set; }
    }
}
