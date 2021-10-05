using System;
using System.Security.Cryptography;

namespace LinearIntersectionCalculator {
    class Program {
        static void Main(string[] args) {

            double xa1, xa2, ya1, ya2, xb1, xb2, yb1, yb2;


            Console.WriteLine("EQUATION 1");

            Console.WriteLine("Enter x1: ");
            xa1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter x2: ");
            xa2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter y1: ");
            ya1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter y2: ");
            ya2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("EQUATION 2");

            Console.WriteLine("Enter x1: ");
            xb1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter x2: ");
            xb2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter y1: ");
            yb1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter y2: ");
            yb2 = Convert.ToDouble(Console.ReadLine());

            double ma, mb;

            ma = (ya2 - ya1) / (xa2 - xa1);
            mb = (yb2 - yb1) / (xb2 - xb1);

            double xf, yf;
            xf = (yb1 - ya1 + (ma * xa1) - (mb * xb1)) / (ma - mb);
            yf = ma * (xf - xa1) + ya1;

            Console.WriteLine("( " + Math.Round(xf) + " , " + Math.Round(yf) + " )");
            
        }
    }
}
