using QueueUp.Domain.Enums;

namespace QueueUp.Domain.Entities;

public class Establishment
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public EEstablishmentType EstablishmentType { get; set; } = EEstablishmentType.Other;
    public string OpenHour { get; set; } = string.Empty;
    public string CloseHour { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
    public Guid AddressId { get; set; } = Guid.Empty;
    public EstablishmentAddress EstablishmentAddress { get; set; } = null!;
}