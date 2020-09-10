using System;

namespace Triangle
{
    public class Program
    {
        public const string UnknownError = "Unknown Error";
        public const string EquilateralTriangle = "Equilateral";
        public const string IsoscelesTriangle = "Isosceles";
        public const string RegularTriangle = "Regular";
        public const string NotTriangle = "Not Triangle";
        public static bool CheckTriangleExistence(int a, int b, int c)
        {
            return a + b > c && b + c > a && a + c > b;
        }

        public static bool IsEquilateral(int a, int b, int c)
        {
            return a == b && b == c;
        }

        public static bool IsIsosceles(int a, int b, int c)
        {

            return a == b || b == c || a == c;
        }

        public static string GetTriangleType(int a, int b, int c)
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
            if (args.Length != 4)
            {
                Console.Write(UnknownError);
                return;
            }

            try
            {
                int a = int.Parse(args[1]);
                int b = int.Parse(args[2]);
                int c = int.Parse(args[3]);

                Console.Write(GetTriangleType(a, b, c));
            }
            catch (FormatException)
            {
                Console.Write(UnknownError);
            }
        }
    }
}
