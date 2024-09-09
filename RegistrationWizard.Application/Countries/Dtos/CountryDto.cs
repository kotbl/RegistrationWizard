namespace RegistrationWizard.Application.Countries.Dtos;

public record CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}