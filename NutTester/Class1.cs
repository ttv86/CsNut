using System;
using System.Collections.Generic;
using OpenTTD;

namespace NutTester
{
    public class Class1 : AIController
    {
        protected override void Start()
        {
            double a = 1d;
            double b = 2d;
            double c = 5d;
            double f1()
            {
                return b / 2d;
            }

            double f2()
            {
                return c / 2d;
            }

            while (true)
            {
                AILog.Info(f2().ToString());
                c++;
                Sleep(10);
            }
        }
    }
}
