namespace QueueUp.Domain.Entities;

public class EstablishmentAddress
{
    public Guid Id { get; set; } = Guid.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public Establishment Establishment { get; set; } = null!;
}