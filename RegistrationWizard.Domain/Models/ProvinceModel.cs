namespace RegistrationWizard.Domain.Models;

public class ProvinceModel : BaseModel
{
    public string Name { get; set; } = null!;
    public IEnumerable<UserModel>? Users { get; set; }

    public int CountryId { get; set; }
    public CountryModel? Country { get; set; }
}