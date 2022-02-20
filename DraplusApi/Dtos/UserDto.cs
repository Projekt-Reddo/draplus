namespace DraplusApi.Dtos;

public class UserView
{
    public string tokenId { get; set; } = null!;
}

public class UserConnection
{
    public string User { get; set; } = null!;
    public string Board { get; set; } = null!;
    public string Chat { get; set; } = null!;
}