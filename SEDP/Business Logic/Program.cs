using SEDP.Business_Logic;
using SEDP.UI;
using System.Diagnostics;
using System.Xml.Linq;

namespace SEDP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gate opengate = new Gate();
            opengate.Security();
        }
    }
}
