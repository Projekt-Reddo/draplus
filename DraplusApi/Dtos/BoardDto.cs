namespace DraplusApi.Dtos
{
    public class BoardCreateDto
    {
        public string UserId { get; set; } = null!;
    }

    public class BoardReadDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<ShapeReadDto> Shapes { get; set; } = null!;
    }


    public class BoardForListDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime LastEdit { get; set; }
        public string ChatRoomId { get; set; } = null!;
    }

    public class BoardForChangeNameDto{
         public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}