namespace QueueUp.Application.Dtos;

public class QueueUserDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid QueueId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public int Position { get; set; } = 0;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}