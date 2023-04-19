using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAssessment;
namespace TestProject
{
    public class MockConsole : IConsole

    {
        private readonly string _output = "Test";

        public string ReadLine()
        {
            return _output;
        }

        public void WriteLine(string message)
        {
            return;
        }

        public void Exit(int code)
        {
            return;
        }
    }
}
