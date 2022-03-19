using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraplusApi.Dtos
{
    public class LineChartDto
    {
        public string Id { get; set; } = null!;
        public string Color { get; set; }
        public IEnumerable<LineData> Data { get; set; } = null!;
    }
    public class LineData
    {
        public string x { get; set; } = null!;
        public int y { get; set; }
    }
}