namespace QueueUp.Domain.Entities;

public class Queue
{
    public Guid Id { get; set; } = Guid.Empty;
    public int Slots { get; set; } = 0;
    public Guid EstablishmentId { get; set; } = Guid.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Establishment Establishment { get; set; } = null!;
    public List<QueueUser> QueueUsers { get; set; } = new List<QueueUser>();
}