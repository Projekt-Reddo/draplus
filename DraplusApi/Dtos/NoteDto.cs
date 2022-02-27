namespace DraplusApi.Dtos;
public class NoteDto
{
    public string Id { get; set; } = "";
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;
    public string Text { get; set; } = "";
}

public class NoteUpdateDto
{
    public string Id { get; set; } = "";
    public string Text { get; set; } = "";
}