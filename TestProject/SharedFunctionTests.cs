using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechAssessment;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class SharedFunctionTests
    {
        InputFile input;
        Dictionary<string, string> arguments;
        [TestInitialize]
        public void Test_Setup()
        {
            arguments = new Dictionary<string, string>
            {
                { "DictionaryFile", "C:\\\\temp\\\\words-english.txt" },
                { "ResultFile", "C:\\\\temp\\\\Results.txt" },
                { "StartWord", "spin" },
                { "EndWord", "spin" }
            };
            input = new InputFile(arguments, new MockConsole());
            SharedFunctions.input = input;
        }

        [TestMethod]
        public void ReadFile_Returns_False()
        {
            input.DictionaryFile = "C:\\\\temp\\\\words - english.txt";
            Assert.IsFalse(SharedFunctions.ReadFile());
        }

        [TestMethod]
        public void ReadFile_Returns_True()
        {
            Assert.IsTrue(SharedFunctions.ReadFile());
        }

        [TestMethod]
        public void CheckArgs_Returns_True()
        {
            Assert.IsTrue(SharedFunctions.Checkargumentsmatch(arguments));
        }

        [TestMethod]
        public void CheckArgs_Returns_False()
        {
            arguments.Remove("EndWord");
            arguments.Add("End1Word", "dddd");
            Assert.IsFalse(SharedFunctions.Checkargumentsmatch(arguments));
        }


        [TestMethod]
        public void CheckStrings_Returns_False()
        {
            arguments.Remove("StartWord");
            arguments.Remove("EndWord");
            arguments.Add("StartWord", "ddd");
            arguments.Add("End1Word", "ddd");
            Assert.IsFalse(SharedFunctions.Checkargumentsmatch(arguments));
        }

        [TestMethod]
        public void CheckStrings_Returns_True()
        {
            arguments.Remove("StartWord");
            arguments.Remove("EndWord");
            arguments.Add("StartWord", "cccc");
            arguments.Add("End1Word", "cccc");
            Assert.IsFalse(SharedFunctions.Checkargumentsmatch(arguments));
        }

        [TestMethod]
        public void WriteFile_Returns_False()
        {
            input.ResultFile = "";
            input.Results = new List<string>();
            Assert.IsFalse(SharedFunctions.WriteFile());
        }


        [TestMethod]
        public void Full_Integration_Test()
        {
            input.StartWord = "code";
            input.EndWord = "dote";
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_Same_Start_End_Word()
        {
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_Not_Real_Word()
        {
            input.StartWord = "cccc";
            input.EndWord = "dddd";
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_Bad_Dictionary()
        {
            input.DictionaryFile = "C:\\\\temp\\\\words - english.txt";
            input.StartWord = "cccc";
            input.EndWord = "dddd";
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_Spin_Spot()
        {
            input.StartWord = "spin";
            input.EndWord = "spot";
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_No_Link()
        {
            input.StartWord = "down";
            input.EndWord = "slow";
            SharedFunctions.Process();
        }

        [TestMethod]
        public void Full_Integration_Test_Invalid_Length()
        {
            input.StartWord = "dow";
            input.EndWord = "slow";
            SharedFunctions.Process();
        }
    }
}
