using QueueUp.Domain.Entities;
using QueueUp.Domain.Enums;

namespace QueueUp.Application.Dtos;

public class EstablishmentDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public double Rating { get; set; } = 0;
    public EEstablishmentType EstablishmentType { get; set; } = EEstablishmentType.Other;
    public bool IsActive { get; set; } = true;
    public string OpenHour { get; set; } = string.Empty;
    public string CloseHour { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public EstablishmentAddressDto EstablishmentAddress { get; set; } = null!;
}