namespace RegistrationWizard.Domain.Models;

public class CountryModel : BaseModel
{
    public string Name { get; set; } = null!;
    public IEnumerable<UserModel>? Users { get; set; }
    public IEnumerable<ProvinceModel>? Provinces { get; set; }
}