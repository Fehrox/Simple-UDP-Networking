using System;
using UdpLanViews.Network;

namespace Tester
{
    
    class Program
    {
        static ViewTester viewTesterOne = new ViewTester(1, "1", true);
        static void Main(string[] args) {
            Network.Listen();
            while (true) {
                Console.WriteLine("Press any key to send message.");
                Console.ReadKey();
                viewTesterOne.ServerAssert();
            }
        }
    }

}
