using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraplusApi.Dtos
{
    public class BarChartDto
    {
        public string Country { get; set; } = null!;
        public float Value { set; get; }
    }
}