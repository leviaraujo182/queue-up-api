namespace QueueUp.Application.Exceptions.Queue;

public class FullQueueException(string message) : Exception(message);