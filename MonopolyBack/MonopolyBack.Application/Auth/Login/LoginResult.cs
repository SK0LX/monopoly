namespace MonopolyBack.Application.Auth.Login;

public record class LoginResult(bool IsSuccess, string? AccessToken);