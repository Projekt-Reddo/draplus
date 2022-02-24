namespace DraplusApi.Dtos;
public class AuthDto
{
    public string Id { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string AccessToken {get; set;} = null!;
    public string BoardId { get; set; } = null!;
    public string ChatRoomId { get; set; } = null!;
}