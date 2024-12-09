using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.UI
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
