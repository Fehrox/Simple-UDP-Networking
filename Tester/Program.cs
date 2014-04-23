using System;
using System.Net;
using UdpNetworking.Client;
using UdpNetworking.Network;
using UdpNetworking.Session;

namespace Tester
{
    
    class Program
    {
        static ViewTester viewTesterOne = new ViewTester(1, 1, "One");
        static ViewTester viewTesterTwo = new ViewTester(2, 1, "Two");
        static void Main(string[] args) {
            //TODO: Impliment console thread processor.
            Session.Find((i, address) => Network.Connect(address));
            viewTesterOne.ViewID = 1;
            viewTesterTwo.ViewID = 2;
            while (true) {
                Console.WriteLine("Press any key to send message.");
                Console.ReadKey();
                viewTesterOne.TestInt++;
                viewTesterTwo.TestInt++;
                viewTesterOne.Sync();
                viewTesterTwo.Sync();
            }
        }
    }

}
