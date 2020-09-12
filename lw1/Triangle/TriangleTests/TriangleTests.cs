using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Triangle;

namespace TriangleTests
{
    [TestClass]
    public class InputFileTests
    {
        [TestMethod]
        public void InputFileTests_WithDifferentArgs()
        {
            string outputFileName = @"output.txt";
            File.WriteAllText(outputFileName, string.Empty);
            using var outputFile = new StreamWriter(outputFileName, true);

            int testCounter = 1;

            using (var inputFile = new StreamReader(@"input.txt"))
            {
                string argsLine;
                while ((argsLine = inputFile.ReadLine()) != null)
                {
                    string[] argsArr = argsLine.Split();
                    string expectedResult = inputFile.ReadLine();

                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    Console.SetError(sw);
                    Program.Main(argsArr);
                    string result = sw.ToString();

                    Assert.AreEqual(result, expectedResult);

                    outputFile.WriteLine($"Test #{testCounter++}: success");
                }
            }
        }
    }
}
