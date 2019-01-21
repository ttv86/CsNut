using System;
using System.Collections.Generic;
using OpenTTD;

namespace NutTester
{
    public class Class1 : AIController
    {
        protected override void Start()
        {
            Method1();
            Method1(1);
            Method1(1, 1);
        }

        public void Method1()
        {

        }
        public void Method1(int a)
        {

        }
        public void Method1(int a, int b)
        {

        }
    }
}
