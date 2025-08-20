namespace QueueUp.Application.Dtos;

public class QueueDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public int Slots { get; set; } = 0;
    public Guid EstablishmentId { get; set; } = Guid.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}