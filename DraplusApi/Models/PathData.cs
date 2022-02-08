namespace DraplusApi.Models
{
    public class PathData
    {
        public int Order { get; set; } = 0;
        public string PointColor { get; set; } = null!;
        public float[][] PointCoordinatePairs { get; set; } = null!;
        public int PointSize { get; set; }
        public bool smooth { get; set; }
        public float[][] SmoothedPointCoordinatePairs { get; set; } = null!;
        public int TailSize { get; set; } = 0;
    }
}