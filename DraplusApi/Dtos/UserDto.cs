namespace DraplusApi.Dtos;

public class UserView
{
    public string tokenId { get; set; } = null!;
}

public class UserConnection
{
    public UserInfoInUserConnection User { get; set; } = null!;
    public string Board { get; set; } = null!;
}

public class UserConnectionChat
{
    public UserInfoInUserConnection User { get; set; } = null!;
    public string Board { get; set; } = null!;
    public string Type = "Chat";
}

public class UserInfoInUserConnection
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Avatar { get; set; } = null!;
}

