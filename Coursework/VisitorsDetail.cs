using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{



   class VisitorsDetail
    {
        public int VisitorsID { get; set; }
        public string VisitorsName { get; set; }
        public string PhoneNo { get; set; }
        public int Price { get; set; }
        public string AgeGroup { get; set; }
        public string Duration { get; set; }
        public int GroupNo { get; set; }
        public DateTime VisitorsDateTime { get; set; }
        public string indateTime { get; set; }
        public string outdateTime { get; set; }
        public string Day { get; set; }
        
        public List<VisitorsDetail> List()
        {
            string d = Utility.ReadToText();
            if (d != null)
            {
                List<VisitorsDetail> lst = JsonConvert.DeserializeObject<List<VisitorsDetail>>(d);
                return lst;
            }
            return null;
        }

    }
}
