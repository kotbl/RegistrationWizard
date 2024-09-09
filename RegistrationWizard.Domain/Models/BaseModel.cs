namespace RegistrationWizard.Domain.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTimeOffset CreateDt { get; set; }
    public DateTimeOffset UpdateDt { get; set; }
}