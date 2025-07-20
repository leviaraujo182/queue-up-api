namespace QueueUp.Application.Exceptions.User;

public class EmailNotAvailableException(string email) : Exception(email);