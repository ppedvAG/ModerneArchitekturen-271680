using System.ComponentModel.DataAnnotations;

namespace EasyBib.WebMvc.Models;

public class MediaItemViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "EAN")]
    [Required(ErrorMessage = "Bitte eine EAN eingeben.")]
    [StringLength(20, ErrorMessage = "Die EAN darf maximal 20 Zeichen lang sein.")]
    public string EAN { get; set; } = string.Empty;
}