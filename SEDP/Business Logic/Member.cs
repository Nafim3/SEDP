using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class Member
    {
        public string Name { get; set; }
        public string BookingNumber { get; set; } // Must be unique.
        public List<Museum> VisitedMuseums { get; set; } = new List<Museum>();

        public void AddVisitedMuseum(Museum museum)
        {
            VisitedMuseums.Add(museum);
        }

        //public decimal CalculateAdditionalCosts()
        //{
        //    decimal additionalCosts = 0;

        //    if (VisitedMuseums.Count > 2)
        //    {
        //        for (int i = 2; i < VisitedMuseums.Count; i++)
        //        {
        //            additionalCosts += VisitedMuseums[i].Cost + 1000;
        //        }
        //    }

        //    return additionalCosts;
        //}


    }
}
