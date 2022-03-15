using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraplusApi.Dtos
{
    public class LineChartDto
    {
        public string LineName { get; set; } = null!;
        public IEnumerable<LineData> Datas { get; set; } = null!;
    }
    public class LineData
    {
        public string Postion { get; set; } = null!;
        public int Value { get; set; }
    }
}