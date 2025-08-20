namespace QueueUp.Domain.Entities;

public class QueueUser
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid QueueId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public int Position { get; set; } = 0;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
    public Queue Queue { get; set; } = null!;
    public User User { get; set; } = null!;
}