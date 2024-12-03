using SEDP.Business_Logic;
using System.Diagnostics;
using System.Xml.Linq;

namespace SEDP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Operation_Handler Handler = new Operation_Handler();
            Handler.Run();
        }
    }
}
