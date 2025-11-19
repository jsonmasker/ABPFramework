using System.ComponentModel.DataAnnotations;

namespace WebCore.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}