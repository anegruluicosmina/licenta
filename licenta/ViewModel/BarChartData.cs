using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class BarChartData
    {
        public string[] labels { get; set; }
        public BarChartDataSet[] datasets { get; set; }
    }
    public class BarChartDataSet
    {
        public string label { get; set; }
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public int borderWidth { get; set; }
        public bool fill { get; set; }
    }
    public class LineChartData
    {
        public DateTime[] labels { get; set; }
        public LineChartDataSet[] datasets { get; set; }
    } 
    public class LineChartDataSet
    {
        public string label { get; set; }
        public string type { get; set; }
        public int[] data { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
        public bool fill { get; set; }
    }
}
