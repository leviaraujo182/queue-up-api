namespace QueueUp.Application.Exceptions.Auth;

public class InvalidEmailOrPasswordException(string message) : Exception(message);