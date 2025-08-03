using System.Text.Json.Serialization;
using QueueUp.Domain.Enums;

namespace QueueUp.Application.Dtos;

public class CreateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EAccountType AccountType { get; set; } = EAccountType.Client;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public AddressDto Address { get; set; } = null!;
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;
}