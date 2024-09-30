namespace WebApplicationReflectionApi.Models;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? UpdatedOn { get; set; } = null;
    public Address? Address { get; set; } = null;

    public List<Address>? Addresses { get; set; }
}