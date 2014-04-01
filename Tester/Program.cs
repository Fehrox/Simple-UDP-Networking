﻿using System;
using UdpLanViews.Network;

namespace Tester
{
    
    class Program
    {
        static ViewTester viewTesterOne = new ViewTester(1, 1, "One");
        static ViewTester viewTesterTwo = new ViewTester(2, 1, "Two");
        static void Main(string[] args) {
            Network.Listen();
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
