using QueueUp.Domain.Enums;

namespace QueueUp.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public EAccountType AccountType { get; set; } = EAccountType.Client;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Guid AddressId { get; set; } = Guid.Empty;
    public Guid? QueueId { get; set; } = null;
    public Address Address { get; set; } = null!;
    public List<Establishment> Establishments { get; set; } = [];
    public List<EstablishmentRating> EstablishmentRatings { get; set; } = [];
    public Queue Queue { get; set; } = null!;
}