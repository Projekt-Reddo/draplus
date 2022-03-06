namespace DraplusApi.Dtos
{
    public class ShapeCreateDto
    {
        public string ClassName { get; set; } = null!;
        public dynamic Data { get; set; } = null!;
    }

    public class ShapeReadDto
    {
        public string Id { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public dynamic Data { get; set; } = null!;
    }

    public class ShapeStack {
        public Stack<ShapeReadDto> UndoStack { get; set; } = new Stack<ShapeReadDto>();
        public Stack<ShapeReadDto> RedoStack { get; set; } = new Stack<ShapeReadDto>();
    }
}