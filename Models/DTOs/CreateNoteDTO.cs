using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotasApi.Models.DTOs
{
    public class CreateNoteDTO
    {
        [Required(ErrorMessage ="Must add Title")]
        [MaxLength(60, ErrorMessage = "Max 60 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Must add Content")]
        [MaxLength(500, ErrorMessage = "Max 500 characters")]
        public string Content { get; set; }
        public int? tagId { get; set; }
    }
}
