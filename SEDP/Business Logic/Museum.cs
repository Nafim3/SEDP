using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class Museum
    {
        public string Name { get; set; }
        public decimal Cost = 2000;
        public List<Member> Visitors { get; set; } = new List<Member>();

        public Museum(string name) 
        {
            Name = name;
        }

    }
}
