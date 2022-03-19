using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraplusApi.Dtos
{
    public class DashboardDTO
    {
        public AdminDto adminDto{ get; set; }
        public IEnumerable<BarChartDto> barChartDto { get; set; }
        public IEnumerable<LineChartDto> lineChartDto{ get; set; }
    }
}