namespace RegistrationWizard.Domain.Models;

public class UserModel : BaseModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public int CountryId { get; set; }
    public CountryModel? Country { get; set; }

    public int ProvinceId { get; set; }
    public ProvinceModel? Province { get; set; }
}