using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Triangle;

namespace TriangleTests
{
    [TestClass]
    public class InputFileTests
    {
        public static void WriteResultToOutputFile(string outputFile, int testCounter)
        {
            using (var tw = new StreamWriter(outputFile, true))
            {
                tw.WriteLine(testCounter + " success");
            }
        }

        [TestMethod]
        public void InputFileTests_WithDifferentArgs()
        {
            string outputFile = @"output.txt";
            File.WriteAllText(outputFile, string.Empty);
            int testCounter = 1;

            using (var sr = new StreamReader("input.txt"))
            {
                string argsLine;
                while ((argsLine = sr.ReadLine()) != null)
                {
                    string[] argsArr = argsLine.Split();
                    string expectedResult = sr.ReadLine();

                    var sw = new StringWriter();
                    Console.SetOut(sw);
                    Console.SetError(sw);
                    Program.Main(argsArr);
                    string result = sw.ToString();

                    Assert.AreEqual(result, expectedResult);

                    WriteResultToOutputFile(outputFile, testCounter++);
                }
            }
        }
    }
}
