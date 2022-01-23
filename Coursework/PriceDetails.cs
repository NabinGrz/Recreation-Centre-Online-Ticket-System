using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class PriceDetails
    {
        public int WeekDays1 { get; set; }
        public int WeekEnds1 { get; set; }
        public int WeekDays2 { get; set; }
      
        public int WeekEnds2 { get; set; }
        public int Group { get; set; }
        public int Duration { get; set; }

    }
}
