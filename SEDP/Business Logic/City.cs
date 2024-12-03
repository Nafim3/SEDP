using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class City
    {
        public string? Name { get; set; }
        public List<Museum> Museums { get; set; } = new List<Museum>();

        public void AddMuseum(Museum museum)
        {
            Museums.Add(museum);
        }

        public void RemoveMuseum(string museumName)
        {
            var museum = Museums.FirstOrDefault(m => m.Name == museumName);
            if (museum != null)
            {
                Museums.Remove(museum);
            }
        }

        public List<string> GetMuseumNames()
        {
            return Museums.Select(m => m.Name).ToList();
        }
    }

}

