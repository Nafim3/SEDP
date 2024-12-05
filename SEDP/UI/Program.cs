using SEDP.Business_Logic;
using SEDP.UI;
using System.Diagnostics;
using System.Xml.Linq;

namespace SEDP
{
    public class Program
    {
        static void Main(string[] args)
        {
            var operationHandler = new Operation_Handler(new Tour_Manager());
            operationHandler.Start();
        }
    }
}
