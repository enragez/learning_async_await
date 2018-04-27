using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.WpfDemo
{
    public class ProgressReportModel
    {
        public bool Async { get; set; }

        public IEnumerable<KeyValuePair<int, long>> ChartResults { get; set; } = Enumerable.Empty<KeyValuePair<int, long>>();
    }
}
