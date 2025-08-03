using System.Text.Json.Serialization;
using QueueUp.Domain.Enums;

namespace QueueUp.Application.Dtos;

public class CreateEstablishmentDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EEstablishmentType EstablishmentType { get; set; } = EEstablishmentType.Other;
    public string OpenHour { get; set; } = string.Empty;
    public string CloseHour { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EstablishmentAddressDto EstablishmentAddress { get; set; } = null!;
}