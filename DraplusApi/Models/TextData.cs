namespace DraplusApi.Models
{
    public class TextData
    {
        public string Color { get; set; } = null!;
        public string Font { get; set; } = null!;
        public int ForceHeight { get; set; } = 0;
        public int ForceWidth { get; set; } = 0;
        public string Text { get; set; } = null!;
        public int V { get; set; }
        public float X { get; set; } = 0f;
        public float Y { get; set; } = 0f;
    }
}