namespace QueueUp.Application.Dtos;

public class AddressDto
{
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
}