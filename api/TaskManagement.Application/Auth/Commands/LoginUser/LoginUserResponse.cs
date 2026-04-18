namespace TaskManagement.Application.Auth.Commands.LoginUser
{
    public record LoginUserResponse(string AccessToken, string RefreshToken);
}
