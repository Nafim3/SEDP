using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SEDP.Business_Logic
{
    [XmlRoot("Tour")]
    public class Tour
    {
        [XmlElement("Name")]
        public string? Name { get; set; }

        [XmlElement("Identifier")]
        public string? Identifier { get; set; }

        [XmlArray("Cities")]
        [XmlArrayItem("City")]
        public List<City> Cities { get; set; } = new List<City>();

        [XmlArray("Members")]
        [XmlArrayItem("Member")]
        public List<Member> Members { get; set; } = new List<Member>();

        public void AddCity(City city)
        {
            Cities.Add(city);
        }

        public void RemoveCity(string? cityName)
        {
            if (cityName != null)
            {
                var cityToRemove = Cities.FirstOrDefault(c => c.Name == cityName);
                if (cityToRemove != null)
                {
                    Cities.Remove(cityToRemove);
                }
            }
        }

        public void AddMember(Member member)
        {
            Members.Add(member);
        }

        public void RemoveMember(string bookingNumber)
        {
            var memberToRemove = Members.FirstOrDefault(m => m.BookingNumber == bookingNumber);
            if (memberToRemove != null)
                Members.Remove(memberToRemove);
        }

        public decimal CalculateExtraCosts()
        {
            decimal totalExtraCosts = 1500;

            foreach (var member in Members)
            {
                if (member.VisitedMuseums.Count > 2)
                {
                    for (int i = 2; i < member.VisitedMuseums.Count; i++)
                    {
                        totalExtraCosts += member.VisitedMuseums[i].Cost + 1800;
                    }
                }
            }
            return totalExtraCosts;
        }
    }
}
