using System;

namespace Triangle
{
    public class Program
    {
        private const string UnknownError = "Unknown Error";
        private const string EquilateralTriangle = "Equilateral";
        private const string IsoscelesTriangle = "Isosceles";
        private const string RegularTriangle = "Regular";
        private const string NotTriangle = "Not Triangle";

        private const double ACCEPTABLE_DELTA = 0.00001;

        private static bool CheckTriangleExistence(double a, double b, double c)
        {
            return a + b > c && b + c > a && a + c > b;
        }

        private static bool IsEquilateral(double a, double b, double c)
        {
            return Math.Abs(a - b) < ACCEPTABLE_DELTA && Math.Abs(b - c) < ACCEPTABLE_DELTA;
        }

        private static bool IsIsosceles(double a, double b, double c)
        {
            return Math.Abs(a - b) < ACCEPTABLE_DELTA || Math.Abs(b - c) < ACCEPTABLE_DELTA || Math.Abs(a - c) < ACCEPTABLE_DELTA;
        }

        private static string GetTriangleType(double a, double b, double c)
        {
            if (CheckTriangleExistence(a, b, c))
            {
                if (IsEquilateral(a, b, c))
                {
                    return EquilateralTriangle;
                }
                if (IsIsosceles(a, b, c))
                {
                    return IsoscelesTriangle;
                }
                return RegularTriangle;
            }
            return NotTriangle;
        }

        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.Write(UnknownError);
                return;
            }

            try
            {
                double a = double.Parse(args[0]);
                double b = double.Parse(args[1]);
                double c = double.Parse(args[2]);

                Console.Write(GetTriangleType(a, b, c));
            }
            catch (FormatException)
            {
                Console.Write(UnknownError);
            }
        }
    }
}
