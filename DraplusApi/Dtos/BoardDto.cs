namespace DraplusApi.Dtos
{
    public class BoardCreateDto
    {
        public string Name { get; set; } = null!;
        public ICollection<ShapeCreateDto> Shapes { get; set; } = null!;
    }

    public class BoardReadDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<ShapeReadDto> Shapes { get; set; } = null!;
    }

}