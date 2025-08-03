namespace QueueUp.Application.Dtos;

public class EstablishmentRatingDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public int Rating { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid EstablishmentId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
}