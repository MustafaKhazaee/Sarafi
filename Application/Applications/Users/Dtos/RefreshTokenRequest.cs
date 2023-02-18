
namespace Sarafi.Application.Applications.Users.Dtos;

public class RefreshTokenRequest
{
    public long? UserId { set; get; }
    public string RefreshToken { set; get; }
}
